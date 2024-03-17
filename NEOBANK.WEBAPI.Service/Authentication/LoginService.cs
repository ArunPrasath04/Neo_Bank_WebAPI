using NEOBANK.WEBAPI.DataAccess.Authentication;
using NEOBANK.WEBAPI.Model.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEOBANK.WEBAPI.Service.Authentication
{
    public class LoginService : ServiceBase
    {
        LoginDataAccess DataAccess { get; set; }

        public LoginService(TokenModel token) : base(token) 
        {
            DataAccess = new LoginDataAccess(GetToken());
        }   

        public async Task<int> LoginVMail (string username, string email)
        {
            var isSuccess = await DataAccess.LoginVMail(username, email);
            return isSuccess;
        }
    }
}
