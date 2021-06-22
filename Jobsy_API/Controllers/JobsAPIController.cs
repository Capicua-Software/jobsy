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

        [HttpGet]
        [Route("api/Jobs/ReadData")]  // Ruta de la API
        public async Task<List<JobsModel>> LoadJobsAsync()
        {
            List<JobsModel> lstJobs = await jobs.LoadJobsAsync(); // Se guarda en una lista el resultado del metodo
            return lstJobs; //Retorna una lista 
        }

        [HttpGet]
        [Route("api/Jobs/GetLastJobs")]  // Ruta de la API
        public async Task<IEnumerable<JobsModel>> GetLastJobsAsync(int index) // Método para cargar en inicio los N ultimos empleos
        {
            IEnumerable<JobsModel> lstJobs = await jobs.GetLastJobsAsync(index); // Se guarda en una lista el resultado del metodo
            return lstJobs; //Retorna una lista 
        }


        [HttpGet]
        [Route("api/Jobs/Loadjob")]  // Ruta de la API
        public async Task<JobsModel> Loadjob(string id)
        {
            JobsModel Job = await jobs.Loadjob(id);
            return Job; //Retorna una lista 
        }


        [HttpPost]
        [Route("api/Jobs/Deletejob")]  // Ruta de la API
        public void Deletejob(string id)
        {
            jobs.Deletejob(id);
        }

    }
}
