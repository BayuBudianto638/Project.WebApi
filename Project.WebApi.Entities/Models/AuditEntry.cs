namespace Project.WebApi.Entities.Models
{
    public partial class AuditEntry
    {
        public decimal Id { get; set; }

        public string? FieldName { get; set; }

        public string? OldValue { get; set; }

        public string? NewValue { get; set; }

        public decimal? AuditId { get; set; }

        public virtual Audit? Audit { get; set; }
    }
}
