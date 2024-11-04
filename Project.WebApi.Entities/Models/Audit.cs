namespace Project.WebApi.Entities.Models
{
    public partial class Audit
    {
        public decimal Id { get; set; }

        public string? Operation { get; set; }

        public string? TableName { get; set; }

        public string? OldValue { get; set; }

        public string? NewValue { get; set; }

        public decimal? RecordId { get; set; }

        public DateTime? ChangeDate { get; set; }

        public decimal? ChangedById { get; set; }

        public virtual ICollection<AuditEntry> AuditEntries { get; set; } = new List<AuditEntry>();
    }
}
