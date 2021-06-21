using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DATA_L.JobsD;
using ENTITY_L.Models.Jobs;

namespace Jobsy_API.Controllers
{
    public class JobsAPIController : ApiController
    {
        JobsD jobs = new JobsD(); // Objeto de la clase Jobs en la capa de datos
       
        [HttpPost] 
        [Route("api/Jobs/PostAJob")]  // Ruta de la API
        public Task<JobsModel> PostAJob(JobsModel model)  // Este metodo sirve para llamar al metodo de la capa datos que se encarga de guardar un empleo
        {
            var _model = jobs.PostJobsAsync(model); // Se guarda en una variable lo que retorna el metodo del API
            return _model; // Retorna la variable
        }

    }
}
