namespace Project.WebApi.Entities.Models
{
    public partial class Menu
    {
        public decimal Id { get; set; }

        public string Name { get; set; } = null!;

        public string Code { get; set; } = null!;

        public string? Description { get; set; }

        public string? Route { get; set; }

        public decimal? ParentNavigationId { get; set; }

        public decimal? Order { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsDeleted { get; set; }

        public string? Icon { get; set; }

        public DateTime? CreatedAt { get; set; }

        public decimal? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public decimal? UpdatedBy { get; set; }

        public DateTime? DeletedAt { get; set; }

        public decimal? DeletedBy { get; set; }

        public virtual User? CreatedByNavigation { get; set; }

        public virtual User? DeletedByNavigation { get; set; }

        public virtual ICollection<Menu> InverseParentNavigation { get; set; } = new List<Menu>();

        public virtual Menu? ParentNavigation { get; set; }

        public virtual ICollection<RoleMenu> RoleMenus { get; set; } = new List<RoleMenu>();

        public virtual User? UpdatedByNavigation { get; set; }
    }
}
