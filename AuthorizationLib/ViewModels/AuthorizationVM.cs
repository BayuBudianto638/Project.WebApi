using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationLib.ViewModels
{
    public class AuthorizationVM
    {
        public bool Auth { get; set; }
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public string UserNpp { get; set; }
        public int? RoleId { get; set; }
        public string? RoleName { get; set; }
        public string? Message { get; set; }
    }
}
