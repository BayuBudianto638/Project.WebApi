namespace UserManagementServices.ViewModels
{
    public class Req_RoleUpdateVM : Req_RoleVM
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
    }
}
