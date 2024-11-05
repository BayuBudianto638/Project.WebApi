using System.Data;

namespace Project.WebApi.Entities.Models
{
    public partial class RoleMenu
    {
        public int Id { get; set; }

        public int? RoleId { get; set; }

        public int? MenuId { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? DeletedAt { get; set; }

        public int? DeletedBy { get; set; }

        public virtual User? CreatedByNavigation { get; set; }

        public virtual User? DeletedByNavigation { get; set; }

        public virtual Menu? Menu { get; set; }

        public virtual Role? Role { get; set; }

        public virtual User? UpdatedByNavigation { get; set; }
    }
}
