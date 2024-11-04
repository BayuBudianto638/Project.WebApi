namespace Project.WebApi.Entities.Models
{
    public partial class UserRole
    {
        public decimal Id { get; set; }

        public decimal? UserId { get; set; }

        public decimal? RoleId { get; set; }

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

        public virtual Role? Role { get; set; }

        public virtual User? UpdatedByNavigation { get; set; }

        public virtual User? User { get; set; }
    }
}
