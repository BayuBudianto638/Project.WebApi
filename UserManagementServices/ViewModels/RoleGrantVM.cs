namespace UserManagementServices.ViewModels
{
    public class RoleGrantVM
    {
        public bool? Create { get; set; } = false;
        public bool? Read { get; set; } = false;
        public bool? Update { get; set; } = false;
        public bool? Delete { get; set; } = false;
    }
}
