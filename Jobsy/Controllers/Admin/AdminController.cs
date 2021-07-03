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
using ENTITY_L.Models.User;
using ENTITY_L.Models.Limite;
using ENTITY_L.Models.Category;

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
        }

        JobsController job = new JobsController();
        UserController user = new UserController();
        CategoryAPIController category = new CategoryAPIController();
        EmployerController employer = new EmployerController();
        AdminAPIController admin = new AdminAPIController();

        // GET: Admin

        Jobs _jobs = new Jobs();

        #region VIEWS

        public ActionResult AdminDashboard()
        {
            // Info.
            return ValidateRole("AdminDashboard");
        }

        // Users
        public async Task<ActionResult> Users()
        {
            ViewBag.AllUsers = await user.LoadUsersAsync();
             return ValidateRole("Users");
        }
        public async Task<ActionResult> Category()
        {
            ViewBag.AllCategory = await category.LoadCategoryAsync();
            return ValidateRole("Category");
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

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Employers()
        {
            ViewBag.AllEmployers = await employer.LoadEmployerAsync();
            return ValidateRole("Employers");
        }

        public async Task<ActionResult> Settings()
        {
            string limite = await admin.getlimit();
            ViewBag.limite = limite;
            return ValidateRole("Settings");
        }
        public ActionResult Profile()
        {
            return ValidateRole("Profile");
        }

        #endregion

        //USERS
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

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> EditLimit(LimiteModel edit)
        {
            try
            {
                await admin.editlimit(edit);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return await Settings();
        }


        // Employers

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


        [HttpPost]
        [AllowAnonymous]
        public ActionResult GetCategory(string id)
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


        public async Task<ActionResult> ExecuteCategoryProcess(string process, string id, CategoryModel model)
        {

            switch (process)
            {
                case "CreateCategory":
                    await category.AddCategory(model);
                    break;

                case "GetCategory":
                    var Categories = await category.LoadCategory(id);
                    return Json(Categories, JsonRequestBehavior.AllowGet);


                case "EditCategory":
                    await category.EditCategory(model);
                    break;

                case "DeleteCategory":
                     category.DeleteCategory(id);
                     return null;                  

            }

            return RedirectToAction("Category");
        }


    }
}
