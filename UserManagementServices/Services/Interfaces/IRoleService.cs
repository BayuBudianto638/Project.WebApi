using Project.WebApi.Entities.Models;

namespace UserManagementServices.Services.Interfaces
{
    public interface IRoleService
    {
        public Task<ResponseBase<List<ViewModels.Res_RoleVM>>> GetAllRoles();
        public Task<ResponseBase<ViewModels.Res_RoleDetailVM>> GetRoleById(int id);
        public Task<ResponseBase<List<ViewModels.Res_MenuVM>>> GetRoleAssignedMenu(int id);
        public Task<ResponseBase<ViewModels.Res_RoleDetailVM>> InsertRole(ViewModels.Req_RoleVM data);
        public Task<ResponseBase<ViewModels.Res_RoleDetailVM>> UpdateRole(ViewModels.Req_RoleUpdateVM data);
        public Task<ResponseBase<ViewModels.Res_RoleVM>> DeleteRole(int id);
    }
}
