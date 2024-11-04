namespace UserManagementServices.ViewModels
{
    public class Res_RoleVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal? Order { get; set; }
        public bool? IsActive { get; set; }
    }
}
