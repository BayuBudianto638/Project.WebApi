namespace UserManagementServices.Services.Interfaces
{
    public class Res_MenuVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
        public string? Route { get; set; }
        public int? ParentNavigationId { get; set; }
        public int? Order { get; set; }
        public string? Icon { get; set; }
        public bool? IsActive { get; set; }
    }
}
