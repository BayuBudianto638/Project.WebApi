using Project.WebApi.Entities.Models;

namespace UserManagementServices.Services.Interfaces
{
    public interface IUserService
    {
        public Task<ResponseBase<List<ViewModels.Res_UserVM>>> GetAllUsers();
        public Task<ResponseBase<ViewModels.Res_UserDetailVM>> GetUserByUsername(string username);
        public Task<ResponseBase<ViewModels.Res_UserDetailVM>> GetUserById(int id);
        public Task<ResponseBase<ViewModels.Res_UserVM>> InsertUser(ViewModels.Req_UserVM data);
        public Task<ResponseBase<ViewModels.Res_UserVM>> DeleteUser(string username);
        public Task<ResponseBase<ViewModels.Res_UserDetailVM>> UpdateUser(ViewModels.Req_UserUpdateVM data);       
        public Task<User> GetUserInfo(string username);
    }
}
