using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Jobsy.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult UserDashboard()
        {
            //string email = ClaimsPrincipal.Current.FindFirst(ClaimTypes.Email).Value;
            //System.Diagnostics.Debug.WriteLine(email);
            return View();
        }
    }
}