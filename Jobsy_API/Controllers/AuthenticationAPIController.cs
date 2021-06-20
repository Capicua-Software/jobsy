using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DATA_L.Authentication;
using ENTITY_L.Models.Authentication;

namespace Jobsy_API.Controllers
{
    public class AuthenticationAPIController : ApiController
    {
        AuthenticationD Auth = new AuthenticationD();

        [HttpPost]
        [Route("api/Auth/Login")]
        public Task<LoginModel> Login(LoginModel model)
        {
            var _model = Auth.LoginAsync(model);
            return _model;
        }

        [HttpPost]
        [Route("api/Auth/SignUp")]
        public Task<SignUpModel> SignUp(SignUpModel model)
        {
            var _model = Auth.RegisterAsync(model);
            return _model;
        }

        [HttpPost]
        [Route("api/SignUpTest")]
        public string SignUpTest(SignUpModel model)
        {
            return $"Su nombre es {model.Name}, su email es: {model.Email} y su contraseña: {model.Password}";
        }
    }
}
