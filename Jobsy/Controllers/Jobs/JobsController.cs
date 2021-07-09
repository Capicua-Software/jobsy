using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ENTITY_L.Models.Jobs;
using Jobsy_API.Controllers;
using Microsoft.Owin.Security;
using System.IO;
using ENTITY_L.Models.Employer;

namespace Jobsy.Controllers
{
    public class JobsController : Controller
    {
        JobsAPIController job = new JobsAPIController();
        UsersAPIController user = new UsersAPIController();
        EmployerController employer = new EmployerController();
        CategoryController category = new CategoryController();

        public async Task<ActionResult> PostAJob()
        {
            EmployerModel employe = await employer.EmployerLoad(ClaimsPrincipal.Current.FindFirst(ClaimTypes.Email).Value);

            if (employe.valido == "true")
            {
                ViewBag.Employer = employe;
                ViewBag.AllCategory = await category.LoadCategories();
                return View();
            }
            else
            {
                return RedirectToAction("EditProfile", "Employer");
            }

        }

        [HttpPost]
        [AllowAnonymous] 
        public async Task<ActionResult> PostAJob(JobsModel model) // Este metodo se llama al enviar el formulario
        {            
               
             await job.PostAJob(model); // Llama al metodo que se encuenta en la API

            return RedirectToAction("Index", "Index");
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> LoadJobsAsync() // Metodo para devolver una vista con todos los empleaos 
        {
            try
            {
               
                ViewBag.AllJobs = await AllJobs(); //Guardamos el resultado del metodo en el Viewbag
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message); //Lanza un mensaje en la consola en caso de error
            }

            return View(); //Retorna la vista
        }


        public async Task<List<JobsModel>> AllJobs()
        {
            List<JobsModel> AllJobs = await job.LoadJobsAsync();// Llama al metodo que se encuenta en la API;
            return AllJobs; 
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> EditJob(string id) 
        {
            try
            {
                ViewBag.Employer = await employer.EmployerLoad(ClaimsPrincipal.Current.FindFirst(ClaimTypes.Email).Value);
                ViewBag.AllCategory = await category.LoadCategories();
                ViewBag.ajob = await LoadJob(id); 
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex); 
            }

            return View(); 
        }


        public async Task<JobsModel> LoadJob(string id)
        {
            JobsModel ajob = await job.Loadjob(id);
            return ajob;
        }

      



        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Edit(JobsModel jobtoedit)
        {
            try
            {
               await job.Editjob(jobtoedit);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return RedirectToAction("Index", "Index");
        }



        [HttpPost]
        [AllowAnonymous]
        public ActionResult Deletejob(string id)
        {
            try
            {
                DeleteAJob(id);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return RedirectToAction("Index", "Index");
        }

        public void DeleteAJob(string id)
        {
          job.Deletejob(id);
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> JobDescription(string id)
        {
            try
            {
                ViewBag.ajob = await LoadJob(id);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return View();
        }


        [HttpGet]
        public async Task<JsonResult> GetUserById(string id)
        {
            var JobInfo = await user.LoadUser(id);

            return Json(JobInfo, JsonRequestBehavior.AllowGet);
        }

    }
}