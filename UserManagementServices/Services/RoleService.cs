using AuthorizationLib.Enums;
using AuthorizationLib.Tools;
using Microsoft.EntityFrameworkCore;
using Project.WebApi.Entities.Data;
using Project.WebApi.Entities.Models;
using UserManagementServices.Services.Interfaces;

namespace UserManagementServices.Services
{
    public class RoleService(IHttpContextAccessor httpContextAccessor, AppDbContext context) : IRoleService
    {
        private readonly AppDbContext _context = context;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly AuthorizationTool _authorizationTool = new AuthorizationTool(context);

        public async Task<ResponseBase<List<ViewModels.Res_RoleVM>>> GetAllRoles()
        {
            try
            {
                var authed = await _authorizationTool.IsAuthorized(_httpContextAccessor.HttpContext.User, AuthGrantEnum.READ);

                if (!authed.Auth)
                    throw new Exception(authed.Message);

                List<ViewModels.Res_RoleVM> roles = await _context.Roles
                    .Where(x => x.IsDeleted == false)
                    .Select(x => RoleToVM(x))
                    .ToListAsync();

                return new ResponseBase<List<ViewModels.Res_RoleVM>> { Status = true, Message = "OK", Data = roles };
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<ViewModels.Res_RoleVM>> { Status = false, Message = ex.Message };
            }
        }

        public async Task<ResponseBase<ViewModels.Res_RoleDetailVM>> GetRoleById(int id)
        {
            try
            {
                var authed = await _authorizationTool.IsAuthorized(_httpContextAccessor.HttpContext.User, AuthGrantEnum.READ);

                if (!authed.Auth)
                    throw new Exception(authed.Message);

                Role? role = await _context.Roles
                    .Where(x => x.Id == id && x.IsDeleted == false)
                    .FirstOrDefaultAsync() ?? throw new Exception("Id not found");

                return new ResponseBase<ViewModels.Res_RoleDetailVM> { Status = true, Message = "OK", Data = await RoleToDetailVM(role) };
            }
            catch (Exception ex)
            {
                return new ResponseBase<ViewModels.Res_RoleDetailVM> { Status = false, Message = ex.Message };
            }
        }

        public async Task<ResponseBase<List<ViewModels.Res_MenuVM>>> GetRoleAssignedMenu(int id)
        {
            try
            {
                var authed = await _authorizationTool.IsAuthorized(_httpContextAccessor.HttpContext.User, AuthGrantEnum.READ);

                if (!authed.Auth)
                    throw new Exception(authed.Message);

                List<ViewModels.Res_MenuVM> menus = await (
                    from m in _context.Menus
                    join rm in _context.RoleMenus on m.Id equals rm.MenuId
                    join r in _context.Roles on rm.RoleId equals r.Id
                    orderby m.Order ascending
                    where
                        r.Id == id &&
                        r.IsActive == true &&
                        r.IsDeleted == false &&
                        (m.Route.Length > 0 || m.Route != "#") &&
                        rm.IsActive == true &&
                        rm.IsDeleted == false
                    select new ViewModels.Res_MenuVM
                    {
                        Id = (int)m.Id,
                        Name = m.Name,
                        Code = m.Code,
                        Description = m.Description,
                        Route = m.Route,
                        ParentNavigationId = (int)m.ParentNavigationId,
                        Order = (int)m.Order,
                        Icon = m.Icon,
                        IsActive = m.IsActive,

                    }).ToListAsync();

                return new ResponseBase<List<ViewModels.Res_MenuVM>> { Status = true, Message = "OK", Data = menus };
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<ViewModels.Res_MenuVM>> { Status = false, Message = ex.Message };
            }
        }

        public async Task<ResponseBase<ViewModels.Res_RoleDetailVM>> InsertRole(ViewModels.Req_RoleVM data)
        {
            try
            {
                var authed = await _authorizationTool.IsAuthorized(_httpContextAccessor.HttpContext.User, AuthGrantEnum.READ);

                if (!authed.Auth)
                    throw new Exception(authed.Message);

                if (data == null)
                    throw new Exception("Body cannot be empty");

                Role newRole = new Role
                {
                    Name = data.Name,
                    Description = data.Description,
                    Order = data.Order,
                    IsActive = data.IsActive,
                    CreatedAt = DateTime.Now,
                    CreatedBy = authed.UserId,
                    UpdatedAt = DateTime.Now,
                    UpdatedBy = authed.UserId
                };

                await _context.Roles.AddAsync(newRole);

                await _context.SaveChangesAsync();

                RoleGrant newRoleGrant = new RoleGrant
                {
                    RoleId = newRole.Id,
                    Create = data.Grants.Create,
                    Read = data.Grants.Read,
                    Update = data.Grants.Update,
                    Delete = data.Grants.Delete
                };

                await _context.RoleGrants.AddAsync(newRoleGrant);

                if (data.Menu.Count > 0)
                {
                    List<RoleMenu> newRoleMenu = (
                        from m in data.Menu
                        select new RoleMenu
                        {
                            RoleId = newRole.Id,
                            MenuId = m.Id,
                            IsActive = true,
                            CreatedBy = authed.UserId,
                            CreatedAt = DateTime.Now
                        }).ToList();

                    await _context.RoleMenus.AddRangeAsync(newRoleMenu);
                }

                await _context.SaveChangesAsync();

                return new ResponseBase<ViewModels.Res_RoleDetailVM> { Status = true, Message = "OK", Data = await RoleToDetailVM(newRole) };
            }
            catch (Exception ex)
            {
                return new ResponseBase<ViewModels.Res_RoleDetailVM> { Status = false, Message = ex.Message };
            }
        }

        public async Task<ResponseBase<ViewModels.Res_RoleDetailVM>> UpdateRole(ViewModels.Req_RoleUpdateVM data)
        {
            try
            {
                var authed = await _authorizationTool.IsAuthorized(_httpContextAccessor.HttpContext.User, AuthGrantEnum.READ);

                if (!authed.Auth)
                    throw new Exception(authed.Message);

                if (data == null)
                    throw new Exception("Body cannot be empty");

                Role? updateRole = await _context.Roles.FindAsync((decimal)data.Id) ?? throw new Exception("Role not found");

                updateRole.Name = data.Name;
                updateRole.Description = data.Description;
                updateRole.Order = data.Order;
                updateRole.IsActive = data.IsActive;
                updateRole.UpdatedBy = authed.UserId;
                updateRole.UpdatedAt = DateTime.Now;

                RoleGrant? updateRoleGrant = await _context.RoleGrants
                    .Where(x => x.RoleId == updateRole.Id)
                    .FirstOrDefaultAsync() ?? throw new Exception("role grant not found");

                updateRoleGrant.Create = data.Grants.Create;
                updateRoleGrant.Read = data.Grants.Read;
                updateRoleGrant.Update = data.Grants.Update;
                updateRoleGrant.Delete = data.Grants.Delete;

                _context.Attach(updateRoleGrant);
                _context.Entry(updateRoleGrant).State = EntityState.Modified;

                _context.Attach(updateRole);
                _context.Entry(updateRole).State = EntityState.Modified;

                if (data.Menu.Any())
                {
                    // Get existing active role menus
                    var currentMenu = await _context.RoleMenus
                        .Where(x => x.RoleId == updateRole.Id && x.IsActive == true && x.IsDeleted == false)
                        .ToListAsync();

                    // Prepare the new menu items
                    var newMenu = data.Menu.Select(x => new RoleMenu
                    {
                        RoleId = updateRole.Id,
                        MenuId = x.Id,
                        IsActive = true,
                        CreatedAt = DateTime.Now,
                        CreatedBy = authed.UserId
                    }).ToList();

                    // Deactivate menus that are in currentMenu but not in newMenu
                    foreach (var menu in currentMenu.Where(cm => !newMenu.Any(nm => nm.MenuId == cm.MenuId)))
                    {
                        menu.IsActive = false;
                        menu.UpdatedAt = DateTime.Now;
                        menu.UpdatedBy = authed.UserId;
                        _context.Entry(menu).State = EntityState.Modified;
                    }

                    // Add new menus that are not in currentMenu
                    foreach (var menu in newMenu.Where(nm => !currentMenu.Any(cm => cm.MenuId == nm.MenuId)))
                    {
                        _context.Entry(menu).State = EntityState.Added;
                    }
                }


                await _context.SaveChangesAsync();

                return new ResponseBase<ViewModels.Res_RoleDetailVM> { Status = true, Message = "OK", Data = await RoleToDetailVM(updateRole) };
            }
            catch (Exception ex)
            {
                return new ResponseBase<ViewModels.Res_RoleDetailVM> { Status = false, Message = ex.Message };
            }
        }

        public async Task<ResponseBase<ViewModels.Res_RoleVM>> DeleteRole(int id)
        {
            try
            {
                var authed = await _authorizationTool.IsAuthorized(_httpContextAccessor.HttpContext.User, AuthGrantEnum.DELETE);

                if (!authed.Auth)
                    throw new Exception(authed.Message);

                if (id < 1)
                    throw new Exception("Body cannot be empty");

                Role? deleteRole = await _context.Roles.FindAsync(id) ?? throw new Exception("Role not found");

                deleteRole.IsActive = false;
                deleteRole.IsDeleted = true;
                deleteRole.DeletedAt = DateTime.Now;
                deleteRole.DeletedBy = authed.UserId;
                deleteRole.UpdatedAt = DateTime.Now;
                deleteRole.UpdatedBy = authed.UserId;

                _context.Attach(deleteRole);
                _context.Entry(deleteRole).State = EntityState.Modified;

                return new ResponseBase<ViewModels.Res_RoleVM> { Status = true, Message = "OK", Data = RoleToVM(deleteRole) };
            }
            catch (Exception ex)
            {
                return new ResponseBase<ViewModels.Res_RoleVM> { Status = false, Message = ex.Message };
            }
        }

        private async Task<ViewModels.Res_RoleDetailVM> RoleToDetailVM(Role data)
        {
            ViewModels.Res_RoleDetailVM role = new ViewModels.Res_RoleDetailVM
            {
                Id = (int)data.Id,
                Name = data.Name,
                Description = data.Description,
                Order = data.Order,
                IsActive = data.IsActive
            };

            var grants = await _context.RoleGrants
                .Where(x => x.RoleId == role.Id)
                .Select(x => new ViewModels.RoleGrantVM
                {
                    Create = x.Create,
                    Read = x.Read,
                    Update = x.Update,
                    Delete = x.Delete
                })
                .FirstOrDefaultAsync();

            role.Grants = grants;

            return role;
        }
        private static ViewModels.Res_RoleVM RoleToVM(Role data)
        {
            return new ViewModels.Res_RoleVM
            {
                Id = (int)data.Id,
                Name = data.Name,
                Description = data.Description,
                Order = data.Order,
                IsActive = data.IsActive
            };
        }
    }
}
