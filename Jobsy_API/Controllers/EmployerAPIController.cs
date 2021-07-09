using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DATA_L.EmployerD;
using ENTITY_L.Models.Employer;
using ENTITY_L.Models.Jobs;
using ENTITY_L.Models.Request;
using ENTITY_L.Models.RNC;
using Newtonsoft.Json;

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

        [HttpGet]
        [Route("api/Employer/Employerjobs")]  // Ruta de la API
        public async Task<List<JobsModel>> employerjobs()
        {
            List<JobsModel> jobs = await employer.employerjobs();
            return jobs; //Retorna una lista 
        }
        public static RNCModel CheckRNC(int RNC)
        {
            string responseBody="";
            var url = $"https://www.webapimetasalud.com/api/General/ConsultarPadron/RNC/{RNC}";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return null;
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            responseBody = objReader.ReadToEnd();   
                        }
                    }
                }

                RNCModel Rnc = JsonConvert.DeserializeObject<RNCModel>(responseBody);
                return Rnc;
            }
            catch (WebException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return null;
            }

        }

        [HttpGet]
        [Route("api/Employer/GetRequestedJobs")]  // Ruta de la API
        public async Task<List<RequestModel>> GetRequestedJobs(string Email)
        {
            List<RequestModel> requests = await employer.Loadrequest(Email);

            return requests; //Retorna una lista 
        }


        [HttpGet]
        [Route("api/Employer/DeleteRequest")]  // Ruta de la API
        public void DeleteRequest(string id)
        {
            employer.DeleteRequest(id);


        }

        
        [HttpPost]
        [Route("api/Employer/ChangeRequestStatus")]  // Ruta de la API
        public void ChangeRequestStatus(string id, string status)
        {
            employer.ChangeRequestStatus(id, status);
        }


    }
}
