using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Jobsy_API.Controllers;
using ENTITY_L.Models.Jobs;

namespace Jobsy.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        JobsController job = new JobsController();
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

        public ActionResult Users()
        {
            return View();
        }

        public ActionResult Employers()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Jobs()
        {
            ViewBag.AllJobs = await job.AllJobs(); //Guardamos el resultado del metodo en el Viewbag
            return View();
        }


        [HttpGet]
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

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> EditJob(string id)
        {
            try
            {
                ViewBag.ajob = await job.LoadJob(id);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return View("JobEdit");
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