using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koton.ECommerce.Core.DTOs
{
    public class LoginResultDto
    {
        public string Token { get; set; }
        public string RoleCode { get; set; }
        public string RoleName { get; set; }
    }

}
