using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NEOBANK.WEBAPI.Model.Authentication;
using NEOBANK.WEBAPI.Service.Authentication;

namespace NEOBANK.WEBAPI.Controllers.Authentication
{
    [ApiController]
    public class LoginController : Controller
    {
        private IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<AuthModel> LoginVMail(LoginModel model)
        {
            TokenModel token = new TokenModel();
            return await new LoginService(token).LoginVMail(model, _config);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/[controller]/create")]
        public async Task<int> CreateUser(LoginModel model)
        {
            TokenModel token = new TokenModel();
            return await new LoginService(token).CreateUser(model);
        }
    }
}
