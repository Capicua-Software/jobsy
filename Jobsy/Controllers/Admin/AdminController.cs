using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Jobsy_API.Controllers;
using ENTITY_L.Models.Jobs;
using ENTITY_L.Models.User;

namespace Jobsy.Controllers
{
    public class AdminController : Controller
    {

        JobsController job = new JobsController();
        UserController user = new UserController();
        EmployerController employer = new EmployerController();

        // GET: Admin
        public ActionResult AdminDashboard()
        {
            try
            {
                // Verification.
                if (Request.IsAuthenticated && ClaimsPrincipal.Current.FindFirst(ClaimTypes.Role).Value == "Admin")
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Index");
                }
            }
            catch (Exception ex)
            {
                // Info
                Console.Write(ex);
            }

            // Info.
            return View();
        }


      

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Jobs()
        {
            ViewBag.AllJobs = await job.AllJobs(); //Guardamos el resultado del metodo en el Viewbag
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult Deletejob(string id)
        {
            try
            {
                job.DeleteAJob(id);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return RedirectToAction("Jobs");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> EditJob(JobsModel model)
        {
            try
            {
                await job.Edit(model);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return RedirectToAction("Jobs");
        }

        [HttpGet]
        public async Task<JsonResult> GetJobById(string id)
        {
            var JobInfo = await job.LoadJob(id);

            return Json(JobInfo, JsonRequestBehavior.AllowGet);
        }

        // Users
        public async Task<ActionResult> Users()
        {
            ViewBag.AllUsers = await user.LoadUsersAsync();
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult DeleteUser(string id)
        {
            try
            {
              user.Delete(id);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return RedirectToAction("Users");
        }


        // Employers

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Employers()
        {
            ViewBag.AllEmployers = await employer.LoadEmployerAsync();
            return View();
        }      

        [HttpPost]
        [AllowAnonymous]
        public ActionResult DeleteEmployer(string id)
        {
            try
            {
                employer.Delete(id);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return RedirectToAction("Employers");
        }




        public ActionResult Admins()
        {
            return View();
        }

        public ActionResult Settings()
        {
            return View();
        }
        public ActionResult Profile()
        {
            return View();
        }



    }
}