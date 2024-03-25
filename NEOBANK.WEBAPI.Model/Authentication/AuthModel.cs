using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEOBANK.WEBAPI.Model.Authentication
{
    public class AuthModel
    {
        [Key]
        public int userId { get; set; }
        public string? accessToken { get; set; }
        public string? username { get; set; }
        public string? fullName { get; set; }
        public int isSuccess { get; set; }
    }
}
