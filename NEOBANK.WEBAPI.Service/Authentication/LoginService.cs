using Microsoft.Extensions.Configuration;
using NEOBANK.WEBAPI.DataAccess.Authentication;
using NEOBANK.WEBAPI.Model.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

        public async Task<AuthModel> LoginVMail (LoginModel model, IConfiguration _config)
        {
            AuthModel authModel = new AuthModel();
            
            if (model.isGoogle == true)
            {
                authModel = await DataAccess.LoginVMail(model.username, model.email, model.password, model.isGoogle);
                if(authModel.isSuccess == 1)
                {
                    var tokenString = new AuthService(model, _config);
                    authModel.accessToken = tokenString.AccessToken;
                }
                return authModel;
            } 
            else
            {
                var emailSalt = await DataAccess.GetSaltbyEmail(model.email);
                if (emailSalt.Length > 0) 
                {
                    byte[] salt = Convert.FromBase64String(emailSalt);
                    model.password = HashPassword(model.password, salt);

                    authModel = await DataAccess.LoginVMail(model.username, model.email, model.password, model.isGoogle);
                    if (authModel.isSuccess == 1)
                    {
                        model.userId = authModel.userId.ToString();
                        var tokenString = new AuthService(model, _config);
                        authModel.accessToken = tokenString.AccessToken;
                    }
                    return authModel;
                }
                else
                {
                    authModel.isSuccess = -3;
                    return authModel;
                }
            }
        }

        public async Task<int> CreateUser(LoginModel model)
        {
            byte[] salt = GenerateSalt();
            var emailHash = Convert.ToBase64String(salt);
            model.password = HashPassword(model.password, salt);

            var isSuccess = await DataAccess.CreateUser(model.username, model.email, model.password, emailHash, model.firstName, model.lastName);
            return isSuccess;
        }

        private byte[] GenerateSalt()
        {
            byte[] salt = new byte[16];
            RandomNumberGenerator.Create().GetBytes(salt);
            return salt;
        }

        private string HashPassword(string password, byte[] salt)
        {
            var deriveBytes = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256);
            return Convert.ToBase64String(deriveBytes.GetBytes(32));
        }
    }
}
