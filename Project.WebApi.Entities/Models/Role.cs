namespace Project.WebApi.Entities.Models
{
    public partial class Role
    {
        public decimal Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public decimal? Order { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedAt { get; set; }

        public decimal? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public decimal? UpdatedBy { get; set; }

        public DateTime? DeletedAt { get; set; }

        public decimal? DeletedBy { get; set; }

        public virtual User? CreatedByNavigation { get; set; }

        public virtual User? DeletedByNavigation { get; set; }
        public virtual ICollection<RoleMenu> RoleMenus { get; set; } = new List<RoleMenu>();
        public virtual ICollection<RoleGrant> RoleGrants { get; set; } = new List<RoleGrant>();

        public virtual User? UpdatedByNavigation { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
