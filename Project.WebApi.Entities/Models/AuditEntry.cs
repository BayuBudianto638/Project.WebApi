namespace Project.WebApi.Entities.Models
{
    public partial class AuditEntry
    {
        public int Id { get; set; }

        public string? FieldName { get; set; }

        public string? OldValue { get; set; }

        public string? NewValue { get; set; }

        public int? AuditId { get; set; }

        public virtual Audit? Audit { get; set; }
    }
}
