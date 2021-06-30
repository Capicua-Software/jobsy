using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DATA_L.EmployerD;
using ENTITY_L.Models.Employer;

namespace Jobsy_API.Controllers
{
    public class EmployerAPIController : ApiController
    {
        EmployerD employer = new EmployerD(); // Objeto de la clase Jobs en la capa de datos

        

        [HttpGet]
        [Route("api/Employer/LoadEmployerAsync")]  // Ruta de la API
        public async Task<List<EmployerModel>> LoadEmployerAsync()
        {
            List<EmployerModel> lsemployer = await employer.LoadEmployerAsync(); // Se guarda en una lista el resultado del metodo
            return lsemployer; //Retorna una lista 
        }
            

        [HttpGet]
        [Route("api/Employer/LoadEmployer")]  // Ruta de la API
        public async Task<EmployerModel> LoadEmployer(string id)
        {
            EmployerModel Employer = await employer.LoadEmployer(id);
            return Employer; //Retorna una lista 
        }

        [HttpPost]
        [Route("api/Employer/EditEmployer")]  // Ruta de la API
        public async Task<EmployerModel> EditEmployer(EmployerModel Employer) // Método para cargar todos los Empleos
        {
            var _employer = await employer.EditEmployer(Employer);
            return _employer;
        }


        [HttpPost]
        [Route("api/Employer/DeleteEmployer")]  // Ruta de la API
        public void DeleteEmployer(string id)
        {
            employer.DeleteEmployer(id);
        }



    }
}
