namespace Project.WebApi.Entities.Models
{
    public partial class Menu
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Code { get; set; } = null!;

        public string? Description { get; set; }

        public string? Route { get; set; }

        public int? ParentNavigationId { get; set; }

        public int? Order { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsDeleted { get; set; }

        public string? Icon { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? DeletedAt { get; set; }

        public int? DeletedBy { get; set; }

        public virtual User? CreatedByNavigation { get; set; }

        public virtual User? DeletedByNavigation { get; set; }

        public virtual ICollection<Menu> InverseParentNavigation { get; set; } = new List<Menu>();

        public virtual Menu? ParentNavigation { get; set; }

        public virtual ICollection<RoleMenu> RoleMenus { get; set; } = new List<RoleMenu>();

        public virtual User? UpdatedByNavigation { get; set; }
    }
}
