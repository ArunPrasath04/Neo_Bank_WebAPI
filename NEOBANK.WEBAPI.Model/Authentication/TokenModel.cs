using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEOBANK.WEBAPI.Model.Authentication
{
    public class TokenModel
    {
        public string? userId { get; set; }
        public string DatabaseConnection { get; set; }
    }
}
