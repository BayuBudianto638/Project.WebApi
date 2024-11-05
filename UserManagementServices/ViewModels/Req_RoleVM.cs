namespace UserManagementServices.ViewModels
{
    public class Req_RoleVM
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? Order { get; set; }
        public RoleGrantVM Grants { get; set; }
        public bool? IsActive { get; set; }
        public List<Res_MenuVM> Menu { get; set; }
    }
}
