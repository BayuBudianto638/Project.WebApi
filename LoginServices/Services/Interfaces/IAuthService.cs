using Project.WebApi.Entities.Models;

namespace LoginServices.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<ResponseBase<ViewModels.Res_AuthVM>> Login(ViewModels.Req_AuthLoginVM data);
        public Task<ResponseBase<ViewModels.Res_AuthVM>> Auth();
        public Task<ResponseBase<ViewModels.Res_AuthRefreshTokenVM>> RefreshToken(ViewModels.Req_AuthRefreshTokenVM data);
        public Task<ResponseBase<List<ViewModels.Res_AuthLoginRoleVM>>> GetRoles();
        public Task<ResponseBase<ViewModels.Res_AuthSetRoleVM>> SetRoleToToken(ViewModels.Req_AuthSetRoleVM data);
        public Task<ResponseBase<string>> Logout();
    }
}
