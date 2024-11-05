using AuthorizationLib.Enums;
using AuthorizationLib.Tools;
using EncryptionLib.Helpers;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Project.WebApi.Entities.Data;
using Project.WebApi.Entities.Models;
using UserManagementServices.Services.Interfaces;

namespace UserManagementServices.Services
{
    public class UserService(IHttpContextAccessor httpContextAccessor, AppDbContext context, IValidator<User> userValidator) : IUserService
    {
        private readonly AppDbContext _context = context;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly AuthorizationTool _authorizationTool = new AuthorizationTool(context);
        private readonly IValidator<User> _userValidator = userValidator;

        public async Task<ResponseBase<List<ViewModels.Res_UserVM>>> GetAllUsers()
        {
            try
            {
                var authed = await _authorizationTool.IsAuthorized(_httpContextAccessor.HttpContext.User, AuthGrantEnum.READ);

                if (!authed.Auth)
                    throw new Exception(authed.Message);

                List<ViewModels.Res_UserVM> users = await _context.Users
                    .Where(x => x.IsDeleted == false)
                    .Select(x => UserToVM(x))
                    .ToListAsync();

                return new ResponseBase<List<ViewModels.Res_UserVM>> { Status = true, Message = "OK", Data = users };

            }
            catch (Exception ex)
            {
                return new ResponseBase<List<ViewModels.Res_UserVM>> { Status = false, Message = ex.Message };
            }
        }

        public async Task<ResponseBase<ViewModels.Res_UserDetailVM>> GetUserByUsername(string username)
        {
            try
            {
                var authed = await _authorizationTool.IsAuthorized(_httpContextAccessor.HttpContext.User, AuthGrantEnum.READ);

                if (!authed.Auth)
                    throw new Exception(authed.Message);

                if (string.IsNullOrEmpty(username))
                    throw new Exception("Invalid body");

                User? user = await _context.Users.Where(x =>
                        x.Username == username &&
                        x.IsDeleted == false).FirstOrDefaultAsync() ?? throw new Exception("Username not found");

                return new ResponseBase<ViewModels.Res_UserDetailVM> { Status = true, Message = "OK", Data = await UserToDetailVM(user) };
            }
            catch (Exception ex)
            {
                return new ResponseBase<ViewModels.Res_UserDetailVM> { Status = false, Message = ex.Message };
            }
        }

        public async Task<ResponseBase<ViewModels.Res_UserDetailVM>> GetUserById(int id)
        {
            try
            {
                var authed = await _authorizationTool.IsAuthorized(_httpContextAccessor.HttpContext.User, AuthGrantEnum.READ);

                if (!authed.Auth)
                    throw new Exception(authed.Message);

                if (id < 1)
                    throw new Exception("Invalid body");

                User? user = await _context.Users.Where(x =>
                        x.Id == id &&
                        x.IsDeleted == false).FirstOrDefaultAsync() ?? throw new Exception("Id not found");

                return new ResponseBase<ViewModels.Res_UserDetailVM> { Status = true, Message = "OK", Data = await UserToDetailVM(user) };

            }
            catch (Exception ex)
            {
                return new ResponseBase<ViewModels.Res_UserDetailVM> { Status = false, Message = ex.Message };
            }
        }

        public async Task<ResponseBase<ViewModels.Res_UserVM>> InsertUser(ViewModels.Req_UserVM data)
        {
            try
            {
                var authed = await _authorizationTool.IsAuthorized(_httpContextAccessor.HttpContext.User, AuthGrantEnum.CREATE);

                if (!authed.Auth)
                    throw new Exception(authed.Message);

                if (data == null)
                    throw new Exception("Invalid body");

                if (await _context.Users.Where(x => x.Username == data.Username).AnyAsync())
                    throw new Exception("Username already exist");

                if (await _context.Users.Where(x => x.Email == data.Email).AnyAsync())
                    throw new Exception("Email already exist");

                User newUser = new User
                {
                    Username = data.Username,
                    Password = GenerateHash(data.Password, data.Username),
                    Fullname = data.Fullname,
                    Email = data.Email,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedAt = DateTime.Now,
                    CreatedBy = authed.UserId,
                    UpdatedAt = DateTime.Now,
                    UpdatedBy = authed.UserId
                };

                if (ValidateUser(newUser).Equals(false))
                    throw new Exception("Please fill all value");

                var registeredUser = await _context.Users.AddAsync(newUser);

                await _context.SaveChangesAsync();

                return new ResponseBase<ViewModels.Res_UserVM> { Status = true, Message = "OK", Data = UserToVM(newUser) };
            }
            catch (Exception ex)
            {
                return new ResponseBase<ViewModels.Res_UserVM> { Status = false, Message = ex.Message };
            }
        }

        public async Task<ResponseBase<ViewModels.Res_UserVM>> DeleteUser(string username)
        {
            try
            {
                var authed = await _authorizationTool.IsAuthorized(_httpContextAccessor.HttpContext.User, AuthGrantEnum.DELETE);

                if (!authed.Auth)
                    throw new Exception(authed.Message);

                if (username.Length < 1)
                    throw new Exception("Id cannot be empty");

                User? removedUser = await _context.Users.Where(x => x.Username == username).FirstOrDefaultAsync();

                if (removedUser == null || removedUser.IsDeleted == true)
                    throw new Exception("Username not found");

                if (removedUser.Id == authed.UserId)
                    throw new Exception("Cannot self delete");

                List<UserRole>? userRoles = await _context.UserRoles.Where(x => x.UserId == removedUser.Id).ToListAsync();

                if (userRoles.Count > 0)
                {
                    userRoles.ForEach(x =>
                    {
                        x.IsDeleted = true;
                        x.IsActive = false;
                        x.DeletedAt = DateTime.Now;
                        x.DeletedBy = authed.UserId;
                        x.UpdatedAt = DateTime.Now;
                        x.UpdatedBy = authed.UserId;
                    });

                    _context.UpdateRange(userRoles);
                }

                removedUser.IsDeleted = true;
                removedUser.DeletedBy = authed.UserId;
                removedUser.DeletedAt = DateTime.Now;
                removedUser.UpdatedBy = authed.UserId;
                removedUser.UpdatedAt = DateTime.Now;

                _context.Attach(removedUser);
                _context.Entry(removedUser).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return new ResponseBase<ViewModels.Res_UserVM> { Status = true, Message = "OK", Data = UserToVM(removedUser) };
            }
            catch (Exception ex)
            {
                return new ResponseBase<ViewModels.Res_UserVM> { Status = false, Message = ex.Message };
            }
        }

        public async Task<ResponseBase<ViewModels.Res_UserDetailVM>> UpdateUser(ViewModels.Req_UserUpdateVM data)
        {
            try
            {
                var authed = await _authorizationTool.IsAuthorized(_httpContextAccessor.HttpContext.User, AuthGrantEnum.UPDATE);

                if (!authed.Auth)
                    throw new Exception(authed.Message);

                if (data == null)
                    throw new Exception("Invalid body");

                User? updateUser = await _context.Users.FindAsync(data.Id) ?? throw new Exception("User not found");

                if (updateUser.Username != data.Username)
                {
                    if (await _context.Users.Where(x => x.Username == data.Username).AnyAsync())
                        throw new Exception("Username already exist");

                    updateUser.Username = data.Username;
                }

                if (updateUser.Email != data.Email)
                {
                    if (await _context.Users.Where(x => x.Email == data.Email).AnyAsync())
                        throw new Exception("Email already exist");

                    updateUser.Email = data.Email;
                }

                updateUser.Password = GenerateHash(data.Password, updateUser.Username);
                updateUser.Fullname = data.Fullname;
                updateUser.IsActive = data.IsActive;
                updateUser.UpdatedAt = DateTime.Now;
                updateUser.UpdatedBy = authed.UserId;

                if (ValidateUser(updateUser).Equals(false))
                    throw new Exception("Please fill all value");

                _context.Attach(updateUser);
                _context.Entry(updateUser).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return new ResponseBase<ViewModels.Res_UserDetailVM> { Status = true, Message = "OK", Data = await UserToDetailVM(updateUser) };
            }
            catch (Exception ex)
            {
                return new ResponseBase<ViewModels.Res_UserDetailVM> { Status = false, Message = ex.Message };
            }
        }

        private static ViewModels.Res_UserVM UserToVM(User data)
        {
            return new ViewModels.Res_UserVM
            {
                Id = (int)data.Id,
                Username = data.Username,
                Fullname = data.Fullname,
                Email = data.Email,
                LastAccess = data.LastAccess
            };
        }

        private async Task<ViewModels.Res_UserDetailVM> UserToDetailVM(User data)
        {
            ViewModels.Res_UserDetailVM userDetail = new ViewModels.Res_UserDetailVM
            {
                Id = (int)data.Id,
                Username = data.Username,
                Fullname = data.Fullname,
                Email = data.Email,
                LastAccess = data.LastAccess,
            };

            var roles = await (
                    from r in _context.Roles
                    join ur in _context.UserRoles on r.Id equals ur.RoleId
                    join rg in _context.RoleGrants on r.Id equals rg.RoleId
                    where ur.UserId == data.Id
                    select new ViewModels.Res_UserLoginRoleVM
                    {
                        Id = (int)r.Id,
                        Name = r.Name,
                        Grants =
                        {
                            Create = rg.Create,
                            Read = rg.Read,
                            Update = rg.Update,
                            Delete = rg.Delete
                        }
                    }).ToListAsync();

            userDetail.Roles = roles.ToArray();

            return userDetail;
        }

        private async Task<User> GetUserInfo(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(x =>
                        x.Username == username &&
                        x.IsDeleted == false)
                        ?? throw new Exception($"User {username} is not registered to CLS System");
        }

        private bool ValidateUser(User user)
        {
            var validationResult = _userValidator.Validate(user);
            return validationResult.IsValid;
        }

        private static string GenerateHash(string password, string salt)
        {
            return Security.GenerateHashWithSalt(password, salt);
        }
    }
}
