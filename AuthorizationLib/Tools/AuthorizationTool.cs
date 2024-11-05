using AuthorizationLib.Enums;
using Microsoft.EntityFrameworkCore;
using Project.WebApi.Entities.Data;
using Project.WebApi.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationLib.Tools
{
    public class AuthorizationTool(AppDbContext context)
    {
        private readonly AppDbContext _context = context;

        public async Task<ViewModels.AuthorizationVM> IsAuthorized(ClaimsPrincipal user, Enums.AuthGrantEnum? grantType)
        {
            try
            {
                var username = user.Identity?.Name;

                if (username is null)
                    throw new Exception("Invalid identity");

                User? authedUser = await _context.Users.Where(x =>
                    x.Username == username &&
                    x.IsActive == true &&
                    x.IsDeleted == false).FirstOrDefaultAsync() ?? throw new Exception("Invalid identity. Username not found");

                var currentRole = user.FindFirstValue(ClaimTypes.Role) ?? throw new Exception("Invalid role");

                Role? role = await _context.Roles
                    .Where(x => x.Id == int.Parse(currentRole) && x.IsActive == true && x.IsDeleted == false)
                    .FirstOrDefaultAsync() ?? throw new Exception("Role not found");

                UserRole? isAssignedRole = await (
                    from ur in _context.UserRoles
                    join r in _context.Roles on ur.RoleId equals r.Id
                    where
                        ur.UserId == authedUser.Id &&
                        ur.IsActive == true &&
                        ur.IsDeleted == false
                    select ur).FirstOrDefaultAsync() ?? throw new Exception("User not assigned to role");

                RoleGrant? roleGrant = await _context.RoleGrants
                   .Where(x => x.RoleId == role.Id).FirstOrDefaultAsync() ?? throw new Exception("Role has no any permission");

                if (grantType == AuthGrantEnum.CREATE)
                    if (roleGrant == null || roleGrant.Create == null || roleGrant.Create == false)
                        throw new Exception("Unauthorized user role");

                if (grantType == AuthGrantEnum.READ)
                    if (roleGrant == null || roleGrant.Read == null || roleGrant.Read == false)
                        throw new Exception("Unauthorized user role");

                if (grantType == AuthGrantEnum.UPDATE)
                    if (roleGrant == null || roleGrant.Update == null || roleGrant.Update == false)
                        throw new Exception("Unauthorized user role");

                if (grantType == AuthGrantEnum.DELETE)
                    if (roleGrant == null || roleGrant.Delete == null || roleGrant.Delete == false)
                        throw new Exception("Unauthorized user role");

                return new ViewModels.AuthorizationVM
                {
                    Auth = true,
                    UserId = (int)authedUser.Id,
                    UserName = authedUser.Fullname,
                    UserNpp = authedUser.Username,
                    RoleId = (int)role.Id,
                    RoleName = role.Name,
                    Message = "OK"
                };
            }
            catch (Exception ex)
            {
                return new ViewModels.AuthorizationVM { Auth = false, Message = ex.Message, UserId = 0 };
            }
        }
    }
}
