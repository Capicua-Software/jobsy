using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Jobsy_API.Controllers;
using ENTITY_L.Models.Jobs;
using System.Security.Claims;
using ENTITY_L.Models.Limite;

namespace Jobsy.Controllers
{
    public class IndexController : Controller
    {
        AdminAPIController admin = new AdminAPIController();
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
                    string limite = await admin.getlimit();
                    IEnumerable<JobsModel> LastJobs = await job.GetLastJobsAsync(int.Parse(limite)); // Llama al metodo que se encuenta en la API
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



        public async Task<ActionResult> Searchjobs()
        {
            ViewBag.AllCategory = await category.LoadCategories();
            ViewBag.JobsFound = JobsFound;
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Search(string categories, string keyword, string process)
        {
            if (categories != null && keyword != "")
            {
               await Searchbycategoryandkeyword(categories, keyword);
            }
            else if (categories != null && keyword == "")
            {
               await SearchByCategory(categories);
            }
            else if (keyword != "" && categories == null)
            {
                await SearchjobsAsync(keyword, process);
            }


            return RedirectToAction("Searchjobs", "Index");

        }



        public async Task SearchjobsAsync(string keyword, string process) // Metodo para devolver una vista con todos los empleaos 
        {
          
            if (process == null && keyword != "" )
            {
                try
                {
                    JobsFound = null;
                    JobsFound = await job.Searchjob(keyword); // Llama al metodo que se encuenta en la API 
                    ViewBag.KeyWord = keyword;
                 
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message); //Lanza un mensaje en la consola en caso de error
                }
            }
            else if (process == "AllJobs"  || process == null && ViewBag.JobsFound == null)
            {
                JobsFound = null;
                JobsFound = await job.LoadJobsAsync();             
                ViewBag.KeyWord = "Todos los empleos";
            }
           
        }

        public async Task SearchByCategory(string category)
        {
            JobsFound = await job.SearchJobByCategory(category);
            ViewBag.JobsFound = JobsFound; //Guardamos el resultado del metodo en el Viewbag           
        }

        public async Task Searchbycategoryandkeyword(string categories, string keyword)
        {
            JobsFound = await job.Searchbycategoryandkeyword(categories,keyword);
            ViewBag.JobsFound = JobsFound; //Guardamos el resultado del metodo en el Viewbag           
        }

    }
}