using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Jobsy_API.Controllers;
using ENTITY_L.Models.Jobs;
using Jobsy.Admin.Jobs;

namespace Jobsy.Controllers
{
    
    public class AdminController : Controller
    {
        //THIS FUNCTION VALIDATES THE USER ROLE TO GIVE ACCESS TO THE DIFFERENT METHODS IN THE CONTROLLER
        public ActionResult ValidateRole(string view)
        {
            if (Request.IsAuthenticated && ClaimsPrincipal.Current.FindFirst(ClaimTypes.Role).Value == "Admin")
            {
                return View(view);
            }
            else
            {
                return RedirectToAction("Index", "Index");
            }

            return View();
        }

        // GET: Admin
        JobsController job = new JobsController();

        Jobs _jobs = new Jobs();

        #region VIEWS

        public ActionResult AdminDashboard()
        {
            // Info.
            return ValidateRole("AdminDashboard");
        }

        public ActionResult Users()
        {
            return ValidateRole("Users");
        }

        public ActionResult Employers()
        {
            return ValidateRole("Employers");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Jobs()
        {
            ViewBag.AllJobs = await job.AllJobs(); //Guardamos el resultado del metodo en el Viewbag
            return ValidateRole("Jobs");
        }


        public ActionResult Admins()
        {
            return ValidateRole("Admins");
        }

        public ActionResult Settings()
        {
            return ValidateRole("Settings");
        }
        public ActionResult Profile()
        {
            return ValidateRole("Profile");
        }

        #endregion

        //JOB PROCESSES
        public async Task<ActionResult> ExecuteJobProcess(string process, string id, JobsModel model)
        {
            switch (process)
            {
                case "Delete":
                    job.DeleteAJob(id);
                    break;

                case "Edit":
                    await job.Edit(model);
                    //RedirectToAction("Jobs");
                    break;

                case "GetJobById":
                    var JobInfo = await job.LoadJob(id);                 
                    return Json(JobInfo, JsonRequestBehavior.AllowGet);
            }

            return RedirectToAction("Jobs");
        }

        
    }
}