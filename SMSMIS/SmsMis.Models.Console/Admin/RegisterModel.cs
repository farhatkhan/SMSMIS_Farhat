using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsMis.Models.Console.Admin
{
    public class RegisterModel
    {
        public List<RoleModel> Roles { get; set; }
    }

    public class RoleModel
    {
        public string RoleName { get; set; }
        public bool Selected { get; set; }
    }
}
