using EncryptionLib.Helpers;
using LoginServices.Services.Interfaces;
using LoginServices.Tools.Interfaces;
using Microsoft.EntityFrameworkCore;
using Project.WebApi.Entities.Data;
using Project.WebApi.Entities.Models;
using System.Security.Claims;
using System.Security.Permissions;

namespace LoginServices.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ITokenTool _tokenTool;
        private readonly string? MasterPassword;

        public AuthService(IHttpContextAccessor httpContextAccessor, AppDbContext context, IConfiguration configuration,
            ITokenTool tokenTool)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _configuration = configuration;
            _tokenTool = tokenTool;
            MasterPassword = _configuration["AppSettings:MasterPassword"];
        }

        public async Task<ResponseBase<ViewModels.Res_AuthVM>> Login(ViewModels.Req_AuthLoginVM data)
        {
            try
            {
                if (data == null)
                    throw new Exception("Invalid body");

                if (string.IsNullOrEmpty(data.Username))
                    throw new Exception("Username cannot be empty");
                if (string.IsNullOrEmpty(data.Password))
                    throw new Exception("Password cannot be empty");

                var user = await IsUserExist(data.Username, data.Password) ?? throw new Exception("Username does not exist");

                var generatedRefreshToken = _tokenTool.GenerateRefreshToken();

                UserToken userToken = new UserToken
                {
                    UserId = user.Id,
                    RefreshToken = generatedRefreshToken,
                    ExpiredAt = DateTime.Now.AddDays(Convert.ToInt32(_configuration["Jwt:ExpirationTime:RefreshToken"])),
                    Ip = data.IP,
                    UserAgent = data.UserAgent
                };

                user.LastAccess = DateTime.Now;

                _context.Attach(user);
                _context.Entry(user).State = EntityState.Modified;

                await _context.AddAsync(userToken);

                await _context.SaveChangesAsync();

                var generatedAccessToken = _tokenTool.GenerateAccessToken(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username)
                });

                return new ResponseBase<ViewModels.Res_AuthVM>
                {
                    Status = true,
                    Message = "OK",
                    Data = new ViewModels.Res_AuthVM
                    {
                        Id = (int)user.Id,
                        Username = user.Username,
                        Email = user.Email,
                        Fullname = user.Fullname,
                        AccessToken = generatedAccessToken,
                        RefreshToken = generatedAccessToken
                    }
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<ViewModels.Res_AuthVM> { Status = false, Message = ex.Message };
            }
        }

        public async Task<ResponseBase<ViewModels.Res_AuthVM>> Auth()
        {
            try
            {
                string? username = _httpContextAccessor.HttpContext?.User.Identity?.Name;

                string? role = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role);

                if (string.IsNullOrEmpty(username))
                    throw new Exception("Invalid token");

                User? user = await IsUserExist(username);

                var selected_role = new ViewModels.Res_AuthLoginRoleVM();

                ViewModels.Res_AuthVM outputUser = new ViewModels.Res_AuthVM
                {
                    Id = (int)user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    Fullname = user.Fullname
                };

                if (!string.IsNullOrEmpty(role))
                {
                    selected_role = await (
                        from r in _context.Roles
                        join ur in _context.UserRoles on r.Id equals ur.RoleId
                        where
                            ur.RoleId == int.Parse(role) &&
                            ur.UserId == user.Id &&
                            ur.IsActive == true &&
                            ur.IsDeleted == false &&
                            r.IsActive == true &&
                            r.IsDeleted == false
                        orderby r.Order
                        select new ViewModels.Res_AuthLoginRoleVM
                        {
                            Id = (int)r.Id,
                            Name = r.Name
                        }).FirstOrDefaultAsync();

                    outputUser.Role = selected_role;
                };

                return new ResponseBase<ViewModels.Res_AuthVM> { Status = true, Message = "OK", Data = outputUser };
            }
            catch (Exception ex)
            {
                return new ResponseBase<ViewModels.Res_AuthVM> { Status = false, Message = ex.Message };
            }
        }

        public async Task<ResponseBase<ViewModels.Res_AuthRefreshTokenVM>> RefreshToken(ViewModels.Req_AuthRefreshTokenVM data)
        {
            try
            {
                if (data == null)
                    throw new Exception("Invalid body");

                var principal = _tokenTool.GetPrincipalFromExpiredToken(data.AccessToken);
                var principalUsername = principal.Identity?.Name;

                string? principalRole = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);

                User? user = await (
                    from u in _context.Users
                    join ut in _context.UserTokens on u.Id equals ut.UserId
                    where
                        ut.RefreshToken == data.RefreshToken &&
                        ut.ExpiredAt >= DateTime.Now &&
                        u.IsActive == true &&
                        u.IsDeleted == false
                    select u).FirstOrDefaultAsync();

                if (user == null || user.Username != principalUsername || user.IsDeleted == true)
                    throw new Exception("Invalid token");

                var selected_role = new ViewModels.Res_AuthLoginRoleVM();

                List<Claim> userClaim = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username)
                };

                if (principalRole.Length > 0)
                {
                    selected_role = await (
                        from r in _context.Roles
                        join ur in _context.UserRoles on r.Id equals ur.RoleId
                        where
                            ur.RoleId == int.Parse(principalRole) &&
                            ur.UserId == user.Id &&
                            ur.IsActive == true &&
                            ur.IsDeleted == false &&
                            r.IsActive == true &&
                            r.IsDeleted == false
                        orderby r.Order
                        select new ViewModels.Res_AuthLoginRoleVM
                        {
                            Id = (int)r.Id,
                            Name = r.Name
                        }).FirstOrDefaultAsync();

                    userClaim.Add(new Claim(ClaimTypes.Role, selected_role.Id.ToString()));
                }

                var generatedAccessToken = _tokenTool.GenerateAccessToken(userClaim);

                return new ResponseBase<ViewModels.Res_AuthRefreshTokenVM>
                {
                    Status = true,
                    Message = "OK",
                    Data = new ViewModels.Res_AuthRefreshTokenVM
                    {
                        AccessToken = generatedAccessToken,
                        RefreshToken = data.RefreshToken
                    }
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<ViewModels.Res_AuthRefreshTokenVM> { Status = false, Message = ex.Message };
            }
        }

        public async Task<ResponseBase<List<ViewModels.Res_AuthLoginRoleVM>>> GetRoles()
        {
            try
            {
                string? username = _httpContextAccessor.HttpContext?.User.Identity?.Name;

                if (string.IsNullOrEmpty(username))
                    throw new Exception("Invalid token");

                User? user = await IsUserExist(username);

                var roles = await (
                    from r in _context.Roles
                    join ur in _context.UserRoles on r.Id equals ur.RoleId
                    where
                        ur.UserId == user.Id &&
                        ur.IsActive == true &&
                        ur.IsDeleted == false &&
                        r.IsActive == true &&
                        r.IsDeleted == false
                    orderby r.Order
                    select new ViewModels.Res_AuthLoginRoleVM
                    {
                        Id = (int)r.Id,
                        Name = r.Name
                    }).ToListAsync();

                return new ResponseBase<List<ViewModels.Res_AuthLoginRoleVM>> { Status = true, Message = "OK", Data = roles };
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<ViewModels.Res_AuthLoginRoleVM>> { Status = false, Message = ex.Message };
            }
        }

        public async Task<ResponseBase<ViewModels.Res_AuthSetRoleVM>> SetRoleToToken(ViewModels.Req_AuthSetRoleVM data)
        {
            try
            {
                string? username = _httpContextAccessor.HttpContext?.User.Identity?.Name;

                if (string.IsNullOrEmpty(username))
                    throw new Exception("Invalid token");

                User? user = await IsUserExist(username);

                if (data == null)
                    throw new Exception("Invalid body");

                if (data.RoleId == null || data.RoleId == 0)
                    throw new Exception("Invalid body. Empty roleId");

                var role = await _IsRoleUserAuthorized((int)user.Id, data.RoleId);

                var generatedAccessToken = _tokenTool.GenerateAccessToken(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, role.Id.ToString())
                });

                return new ResponseBase<ViewModels.Res_AuthSetRoleVM>
                {
                    Status = true,
                    Message = "OK",
                    Data = new ViewModels.Res_AuthSetRoleVM
                    {
                        AccessToken = generatedAccessToken
                    }
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<ViewModels.Res_AuthSetRoleVM> { Status = false, Message = ex.Message };
            }
        }

        public async Task<ResponseBase<string>> Logout()
        {
            try
            {
                var currentToken = GetCurrentToken();
                if (currentToken == null)
                {
                    return new ResponseBase<string> { Status = false, Message = "No token found" };
                }

                _tokenTool.InvalidateToken(currentToken);

                return new ResponseBase<string> { Status = true, Message = "Logout successful" };
            }
            catch (Exception ex)
            {
                return new ResponseBase<string> { Status = false, Message = ex.Message };
            }
        }

        private async Task<ViewModels.Res_AuthLoginRoleVM> _IsRoleUserAuthorized(int userId, int roleId)
        {
            return await (
                    from r in _context.Roles
                    join ur in _context.UserRoles on r.Id equals ur.RoleId
                    where
                        ur.UserId == userId &&
                        ur.IsActive == true &&
                        ur.IsDeleted == false &&
                        r.IsActive == true &&
                        r.IsDeleted == false &&
                        r.Id == roleId
                    orderby r.Order
                    select new ViewModels.Res_AuthLoginRoleVM
                    {
                        Id = (int)r.Id,
                        Name = r.Name
                    }).FirstOrDefaultAsync() ?? throw new Exception("Unauthorized role");
        }
        private async Task<User> IsUserExist(string userName, string password)
        {
            var user = await _context.Users.Where(x =>
                x.Username == userName &&
                x.Password == Security.GenerateHashWithSalt(password, userName) &&
                x.IsActive == true &&
                x.IsDeleted == false).FirstOrDefaultAsync();

            return user ?? null;
        }

        private async Task<User> IsUserExist(string userName)
        {
            var user = await _context.Users.Where(x =>
                x.Username == userName &&
                x.IsActive == true &&
                x.IsDeleted == false).FirstOrDefaultAsync();

            return user ?? new User();
        }

        private string? GetCurrentToken()
        {
            var authHeader = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].FirstOrDefault();
            if (authHeader != null && authHeader.StartsWith("Bearer "))
            {
                return authHeader.Substring("Bearer ".Length).Trim();
            }
            return null;
        }

        private bool IsUserHasRole(int id)
        {
            return _context.UserRoles.Any(x => x.UserId == id);
        }
    }
}
