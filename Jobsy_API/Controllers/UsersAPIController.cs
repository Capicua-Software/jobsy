using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DATA_L.UserD;
using DATA_L.Request;
using ENTITY_L.Models.User;
using ENTITY_L.Models.Request;
using System.Web.Http.Results;

namespace Jobsy_API.Controllers
{
    public class UsersAPIController : ApiController
    {
        UserD user = new UserD(); // Objeto de la clase Jobs en la capa de datos

        RequestD request = new RequestD();

        [HttpGet]
        [Route("api/Users/LoadUsersAsync")]  // Ruta de la API
        public async Task<List<UserModel>> LoadUsersAsync()
        {
            List<UserModel> lstusers = await user.LoadUsersAsync(); // Se guarda en una lista el resultado del metodo
            return lstusers; //Retorna una lista 
        }
            

        [HttpGet]
        [Route("api/Users/LoadUser")]  // Ruta de la API
        public async Task<UserModel> LoadUser(string id)
        {
            UserModel User = await user.LoadUser(id);
            return User; //Retorna una lista 
        }

        [HttpPost]
        [Route("api/Users/EditUser")]  // Ruta de la API
        public async Task<UserModel> EditUser(UserModel User) // Método para cargar todos los Empleos
        {
            var _user = await user.EditUser(User);
            return _user;
        }


        [HttpPost]
        [Route("api/Users/DeleteUser")]  // Ruta de la API
        public void DeleteUser(string id)
        {
            user.DeleteUser(id);
        }

        [HttpGet]
        [Route("api/Users/GetRequestedJobs")]  // Ruta de la API
        public async Task<List<RequestModel>> GetRequestedJobs(string cedula)
        {
            List<RequestModel > requests = await request.Loadrequest(cedula);

            return requests; //Retorna una lista 
        }




    }
}
