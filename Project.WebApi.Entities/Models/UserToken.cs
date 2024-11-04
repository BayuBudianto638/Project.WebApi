namespace Project.WebApi.Entities.Models
{
    public partial class UserToken
    {
        public decimal Id { get; set; }

        public decimal UserId { get; set; }

        public string RefreshToken { get; set; } = null!;

        public string? Ip { get; set; }

        public string? UserAgent { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? ExpiredAt { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
