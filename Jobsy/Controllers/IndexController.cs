using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Jobsy_API.Controllers;
using ENTITY_L.Models.Jobs;
using System.Security.Claims;

namespace Jobsy.Controllers
{
    public class IndexController : Controller
    {

        JobsAPIController job = new JobsAPIController();
        static IEnumerable<JobsModel> LastJobs = null;
        // GET: Index
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Index()
        {
            if (LastJobs == null)
            {
                try
                {
                    IEnumerable<JobsModel> LastJobs = await job.GetLastJobsAsync(3); // Llama al metodo que se encuenta en la API
                    ViewBag.LastJobs = LastJobs; //Guardamos el resultado del metodo en el Viewbag
                    if (User.Identity.IsAuthenticated)
                    {
                        ViewBag.Name = ClaimsPrincipal.Current.FindFirst(ClaimTypes.Name).Value;
                        ViewBag.Role = ClaimsPrincipal.Current.FindFirst(ClaimTypes.Role).Value;
                    }                    

                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message); //Lanza un mensaje en la consola en caso de error
                }
            }
            else
            {
                ViewBag.LastJobs = LastJobs;
                LastJobs = null;
            }
            

            return View(); //Retorna la vista
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Searchjobs(string keyword) // Metodo para devolver una vista con todos los empleaos 
        {
            try
            {
                LastJobs = await job.Searchjob(keyword); // Llama al metodo que se encuenta en la API
                ViewBag.LastJobs = LastJobs; //Guardamos el resultado del metodo en el Viewbag
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message); //Lanza un mensaje en la consola en caso de error
            }

            return RedirectToAction("Index"); //Retorna la vista
        }

    }
}
