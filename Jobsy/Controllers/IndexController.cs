using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Jobsy_API.Controllers;
using ENTITY_L.Models.Jobs;

namespace Jobsy.Controllers
{
    public class IndexController : Controller
    {

        JobsAPIController job = new JobsAPIController();
        // GET: Index
        public async Task<ActionResult> Index()
        {
            try
            {
                IEnumerable<JobsModel> LastJobs = await job.GetLastJobsAsync(3); // Llama al metodo que se encuenta en la API
                ViewBag.LastJobs = LastJobs; //Guardamos el resultado del metodo en el Viewbag
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message); //Lanza un mensaje en la consola en caso de error
            }

            return View(); //Retorna la vista
        }



    }
}
