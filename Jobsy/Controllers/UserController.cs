using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using ENTITY_L.Models.User;
using ENTITY_L.Models.Request;
using Jobsy_API.Controllers;
using Microsoft.Owin.Security;
using System.IO;
using System.Threading.Tasks;

namespace Jobsy.Controllers
{
    public class UserController : Controller
    {
        UsersAPIController user = new UsersAPIController();
        public ActionResult UserDashboard()
        {
            //string email = ClaimsPrincipal.Current.FindFirst(ClaimTypes.Email).Value;
            //System.Diagnostics.Debug.WriteLine(email);
            try
            {
                // Verification.
                if (Request.IsAuthenticated && ClaimsPrincipal.Current.FindFirst(ClaimTypes.Role).Value == "User")
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
        public async Task<List<UserModel>> LoadUsersAsync() // Metodo para devolver una vista con todos los empleaos 
        {
            try
            {
                return await user.LoadUsersAsync(); //Guardamos el resultado del metodo en el Viewbag
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message); //Lanza un mensaje en la consola en caso de error
            }

            return null;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> LoadUser(string id)
        {
            try
            {              
                ViewBag.auser = await UserLoad(id);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return View();
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<UserModel> UserLoad(string id)
        {
            try
            {
                UserModel auser = await user.LoadUser(id);
                return auser;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
            return null;
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> EditUser(UserModel usertoedit)
        {
            try
            {
               await Edit(usertoedit);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return RedirectToAction("LoadUsersAsync");
        }



        [HttpPost]
        [AllowAnonymous]
        public async Task<UserModel> Edit(UserModel usertoedit)
        {
            await user.EditUser(usertoedit);
            return usertoedit;
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult DeleteUser(string id)
        {
            try
            {
                Delete(id);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return RedirectToAction("LoadUsersAsync");
        }

        public void Delete(string id)
        {
            try
            {
                user.DeleteUser(id);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

        }

        public async Task<ActionResult> EditProfile() 
        {
            ViewBag.User = await UserLoad(ClaimsPrincipal.Current.FindFirst(ClaimTypes.Email).Value);
            return View(); 
        }

        public async Task<ActionResult> RequestViewer()
        {
            ViewBag.RequestedJobs = await RequestLoader();
            try
            {
                ViewBag.RequestedJobs = await RequestLoader();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message); //Lanza un mensaje en la consola en caso de error
            }

            return View();
        }

        public async Task<List<RequestModel>> RequestLoader()
        {
            string Email = ClaimsPrincipal.Current.FindFirst(ClaimTypes.Email).Value;
            List<RequestModel> requestModel = await user.GetRequestedJobs(Email);

            return requestModel;
        }


        public async Task<ActionResult> ProfileEdit(UserModel model, HttpPostedFileBase Logo)
        {

                if (Logo != null)
                {
                    model.Logo = Logo.FileName;
                    bool exists = System.IO.Directory.Exists(Server.MapPath("~/Uploads/"));

                    if (!exists) System.IO.Directory.CreateDirectory(Server.MapPath("~/Uploads/"));
                Logo.SaveAs(Server.MapPath("~/Uploads/" + model.Logo));
                }
                model.Id = ClaimsPrincipal.Current.FindFirst(ClaimTypes.Email).Value;
                await user.EditUser(model);           

            return RedirectToAction("EditProfile");
        }

    }
}