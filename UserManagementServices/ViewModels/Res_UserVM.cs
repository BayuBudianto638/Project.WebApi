namespace UserManagementServices.ViewModels
{
    public class Res_UserVM
    {
        public int Id { get; set; }

        public string Username { get; set; } = null!;

        public string Fullname { get; set; } = null!;

        public string Email { get; set; } = null!;

        public DateTimeOffset? LastAccess { get; set; }
    }
}
