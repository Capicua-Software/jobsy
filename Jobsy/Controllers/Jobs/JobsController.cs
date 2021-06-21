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
        public async Task<ActionResult> PostAJob(JobsModel model) // Este metodo se llama al enviar el formulario
        {
            try
            {
                await job.PostAJob(model); // Llama al metodo que se encuenta en la API
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return View();
        }
    }
}