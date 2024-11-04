using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data;

namespace Project.WebApi.Entities.Models
{
    public partial class User
    {
        public decimal Id { get; set; }

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Fullname { get; set; } = null!;

        public string Email { get; set; } = null!;

        public DateTime? LastAccess { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedAt { get; set; }

        public decimal? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public decimal? UpdatedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public decimal? DeletedBy { get; set; }
        public virtual ICollection<Menu> MenuCreatedByNavigations { get; set; } = new List<Menu>();

        public virtual ICollection<Menu> MenuDeletedByNavigations { get; set; } = new List<Menu>();

        public virtual ICollection<Menu> MenuUpdatedByNavigations { get; set; } = new List<Menu>();
        public virtual ICollection<Role> RoleCreatedByNavigations { get; set; } = new List<Role>();
        public virtual ICollection<Role> RoleDeletedByNavigations { get; set; } = new List<Role>();

        public virtual ICollection<RoleMenu> RoleMenuCreatedByNavigations { get; set; } = new List<RoleMenu>();

        public virtual ICollection<RoleMenu> RoleMenuDeletedByNavigations { get; set; } = new List<RoleMenu>();

        public virtual ICollection<RoleMenu> RoleMenuUpdatedByNavigations { get; set; } = new List<RoleMenu>();
        public virtual ICollection<Role> RoleUpdatedByNavigations { get; set; } = new List<Role>();

        public virtual User? UpdatedByNavigation { get; set; }

        public virtual ICollection<UserRole> UserRoleCreatedByNavigations { get; set; } = new List<UserRole>();

        public virtual ICollection<UserRole> UserRoleDeletedByNavigations { get; set; } = new List<UserRole>();

        public virtual ICollection<UserRole> UserRoleUpdatedByNavigations { get; set; } = new List<UserRole>();

        public virtual ICollection<UserRole> UserRoleUsers { get; set; } = new List<UserRole>();

        public virtual ICollection<UserToken> UserTokens { get; set; } = new List<UserToken>();
    }
}
