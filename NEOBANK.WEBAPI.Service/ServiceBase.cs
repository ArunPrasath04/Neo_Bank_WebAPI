using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEOBANK.WEBAPI.Model.Authentication;

namespace NEOBANK.WEBAPI.Service
{
    public class ServiceBase
    {
        public string? userId;

        private TokenModel? token = null;

        public ServiceBase(TokenModel _token) 
        {
            this.token = _token;
            userId = token.userId;
        }
        public TokenModel? GetToken()
        {
            return token;
        }

        public void SetToken(TokenModel _token)
        {
            this.token = _token;
            userId = token.userId;
        }
        
    }

}
