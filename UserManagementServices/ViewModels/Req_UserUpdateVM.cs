namespace UserManagementServices.ViewModels
{
    public class Req_UserUpdateVM : Req_UserVM
    {
        public int Id { get; set; }
        public bool? IsActive { get; set; }
    }
}
