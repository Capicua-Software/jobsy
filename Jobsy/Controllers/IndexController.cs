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
        CategoryController category = new CategoryController();
        static IEnumerable<JobsModel> LastJobs = null;
        static IEnumerable<JobsModel> JobsFound = null;
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
        public async Task<ActionResult> Searchjobs(string keyword, string process) // Metodo para devolver una vista con todos los empleaos 
        {
            ViewBag.AllCategory = await category.LoadCategories();
            ViewBag.JobsFound = JobsFound;

            if (process == null && keyword != "" && ViewBag.JobsFound == null)
            {
                try
                {
                    JobsFound = await job.Searchjob(keyword); // Llama al metodo que se encuenta en la API
                  
                    ViewBag.JobsFound = JobsFound; //Guardamos el resultado del metodo en el Viewbag
                    ViewBag.KeyWord = keyword;
                 
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message); //Lanza un mensaje en la consola en caso de error
                }
            }
            else if (process == "AllJobs"  || process == null && ViewBag.JobsFound == null)
            {
                JobsFound = await job.LoadJobsAsync();
                ViewBag.JobsFound = JobsFound;
                ViewBag.KeyWord = "Todos los empleos";
            }

            JobsFound = null;
            return View(); //Retorna la vista
        }

        public async Task<ActionResult> SearchByCategory(string keyword)
        {
            JobsFound = await job.SearchJobByCategory(keyword);
            ViewBag.JobsFound = JobsFound; //Guardamos el resultado del metodo en el Viewbag
            return RedirectToAction("Searchjobs", "Index");
        }

    }
}
