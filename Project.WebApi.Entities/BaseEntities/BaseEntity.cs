using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Project.WebApi.Entities.BaseEntities
{
    public class BaseEntity
    {        
        public bool? IsActive { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTimeOffset? CreatedAt { get; set; }

        public int? CreatedBy { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTimeOffset? DeletedAt { get; set; }

        public int? DeletedBy { get; set; }
    }
}
