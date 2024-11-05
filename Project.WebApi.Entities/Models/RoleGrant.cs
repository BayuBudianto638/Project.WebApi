namespace Project.WebApi.Entities.Models
{
    public partial class RoleGrant
    {
        public int Id { get; set; }

        public int? RoleId { get; set; }

        public bool? Create { get; set; }

        public bool? Read { get; set; }

        public bool? Update { get; set; }

        public bool? Delete { get; set; }

        public virtual Role? Role { get; set; }
    }
}
