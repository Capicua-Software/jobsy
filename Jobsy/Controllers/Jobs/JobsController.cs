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

namespace Jobsy.Controllers
{
    public class JobsController : Controller
    {
        JobsAPIController job = new JobsAPIController();


        public ActionResult PostAJob()
        {
            return View();
        }
            
        [HttpPost]
        [AllowAnonymous] 
        public async Task<ActionResult> PostAJob(JobsModel model, HttpPostedFileBase Logo) // Este metodo se llama al enviar el formulario
        {
            
                if (Logo != null) 
                { 
                    model.Logo = Logo.FileName;
                    Logo.SaveAs(Server.MapPath("~/Uploads/" + model.Logo));
                }
                await job.PostAJob(model); // Llama al metodo que se encuenta en la API
          
            return View();//Retorna la vista
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

            return RedirectToAction("LoadJobsAsync");
        }



        [HttpGet]
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

            return RedirectToAction("LoadJobsAsync");
        }

        public void DeleteAJob(string id)
        {
          job.Deletejob(id);
        }

       

    }
}