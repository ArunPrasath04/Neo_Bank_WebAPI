using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEOBANK.WEBAPI.Model.Authentication
{
    public class LoginModel
    {
        [Key]
        public string email { get; set; }

        public string username { get; set; }

        public string? userId { get; set; }
    }
}
