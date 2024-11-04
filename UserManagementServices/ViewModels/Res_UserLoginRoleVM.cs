using Project.WebApi.Entities.Models;

namespace UserManagementServices.ViewModels
{
    public class Res_UserLoginRoleVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public RoleGrantVM Grants { get; set; }
    }
}
