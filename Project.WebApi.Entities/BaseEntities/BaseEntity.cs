using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Project.WebApi.Entities.BaseEntities
{
    public class BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public bool? IsActive { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedAt { get; set; }

        public decimal? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public decimal? UpdatedBy { get; set; }

        public DateTime? DeletedAt { get; set; }

        public decimal? DeletedBy { get; set; }
    }
}
