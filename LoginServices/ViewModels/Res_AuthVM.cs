namespace LoginServices.ViewModels
{
    public class Res_AuthVM
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public Res_AuthLoginRoleVM? Role { get; set; }
    }
}
