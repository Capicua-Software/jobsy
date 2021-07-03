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
        //remember to fix this when you start a pull request to avoid merge conflicts
        CategoryController categories = new CategoryController();

        JobsAPIController job = new JobsAPIController();
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
        public async Task<ActionResult> Searchjobs(string keyword, string process, string category) // Metodo para devolver una vista con todos los empleaos 
        {
            ViewBag.AllCategory = await categories.LoadCategories();

              if (process == null && keyword != "")
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
              else if (process == "AllJobs" || process == null)
              {
                  JobsFound = await job.LoadJobsAsync();
                  ViewBag.JobsFound = JobsFound;
                  ViewBag.KeyWord = "Todos los empleos";
              }
              else if (process == null && keyword != "" && category != "")
              {
                  JobsFound = await job.SearchJobByCategory(category);
                  ViewBag.JobsFound = JobsFound; //Guardamos el resultado del metodo en el Viewbag
                  ViewBag.KeyWord = category;
              } 


            return View(); //Retorna la vista
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> SearchJobsWithCategory(string keyword, string process, string category) // Metodo para devolver una vista con todos los empleaos 
        {

            return View();
        }




    }
}
