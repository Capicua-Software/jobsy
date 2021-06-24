using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Jobsy.Controllers
{
    public class EmployerController : Controller
    {
        // GET: Employer
        public ActionResult EmployerDashboard()
        {
            try
            {
                // Verification.
                if (Request.IsAuthenticated && ClaimsPrincipal.Current.FindFirst(ClaimTypes.Role).Value == "Employer")
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
    }
}