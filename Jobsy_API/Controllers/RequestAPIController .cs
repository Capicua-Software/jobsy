using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DATA_L.Request;
using ENTITY_L.Models.Request;

namespace Jobsy_API.Controllers
{
    public class RequestAPIController : ApiController
    {
        RequestD request = new RequestD(); // Objeto de la clase Jobs en la capa de datos

        [HttpPost]
        [Route("api/Request/SendRequest")]  // Ruta de la API
        public Task<RequestModel> SendRequest(RequestModel model)  // Este metodo sirve para llamar al metodo de la capa datos que se encarga de guardar un empleo
        {
            var _model = request.SendRequest(model); // Se guarda en una variable lo que retorna el metodo del API
            return _model; // Retorna la variable
        }

    

        [HttpPost]
        [Route("api/Request/DeleteRequest")]  // Ruta de la API
        public void DeleteRequest(string id)
        {
            request.DeleteRequest(id);
        }

        [HttpGet]
        [Route("api/Request/LoadRequest")]  // Ruta de la API
        public async Task<List<RequestModel>> LoadRequest(string idjob)
        {
           return await request.Loadrequest(idjob);
        }




    }
}
