using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ENTITY_L.Models.Authentication;
using Jobsy_API.Controllers;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Jobsy.Controllers
{
    public class AuthController : Controller
    {
        AuthenticationAPIController Auth = new AuthenticationAPIController();
        private LoginModel _model;

        // GET: Auth
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult SignUp()
        {
            try
            {
                // Verification.
                if (this.Request.IsAuthenticated)
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


        //If user is logged in and tries to go to login page, it will redirect page
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            try
            {
                // Verification.
                if (this.Request.IsAuthenticated)
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

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginModel model, string returnUrl)
        {
            try
            {
                // Verification.
                if (ModelState.IsValid)
                {
                    _model = await Auth.Login(model);

                    if (_model.token != "")
                    {
                        this.SignInUser(_model, false);
                        return RedirectToLocal(model.Role,returnUrl);
                    }
                    else
                    {
                        // Setting.
                        ModelState.AddModelError(string.Empty, "Email o contraseña inválidos.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Info
                Console.Write(ex);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //Cookie
        private void SignInUser(LoginModel userData, bool isPersistent)
        {
            // Initialization.
            var claims = new List<Claim>();

            try
            {
                // Setting
                claims.Add(new Claim(ClaimTypes.Email, userData.Email));
                claims.Add(new Claim(ClaimTypes.Authentication, userData.token));
                claims.Add(new Claim(ClaimTypes.Role, userData.Role));
                claims.Add(new Claim(ClaimTypes.Name, userData.UserName));

                claims.Add(new Claim(ClaimTypes.NameIdentifier, userData.Cedula));
                claims.Add(new Claim(ClaimTypes.Actor, userData.Logo));
                var claimIdenties = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                var ctx = Request.GetOwinContext();
                var authenticationManager = ctx.Authentication;
                // Sign In.
                authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, claimIdenties);
            }
            catch (Exception ex)
            {
                // Info
                throw ex;
            }
        }

        public ActionResult Redirect()
        {
            string Role = ClaimsPrincipal.Current.FindFirst(ClaimTypes.Role).Value;

            switch (Role)
            {
                case "Admin":
                    return this.RedirectToAction("AdminDashboard", "Admin");
                case "Employer":
                    return this.RedirectToAction("EmployerDashboard", "Employer");
                case "User":
                    return this.RedirectToAction("UserDashboard", "User");
            }

            return RedirectToAction("Index", "Index");
        }

        private ActionResult RedirectToLocal(string Role ,string returnUrl)
        {
            try
            {
                // Verification.
                if (!Url.IsLocalUrl(returnUrl))
                {
                    switch (Role)
                    {
                        case "Admin":
                            return this.RedirectToAction("AdminDashboard", "Admin");
                        case "Employer":
                            return this.RedirectToAction("EmployerDashboard", "Employer");
                        case "User":
                            return this.RedirectToAction("UserDashboard", "User");
                    }
                    
                }
                else if (Url.IsLocalUrl(returnUrl))
                {
                    return this.Redirect(returnUrl);
                }
            }
            catch (Exception ex)
            {
                // Info
                throw ex;
            }

            // Info.
            return this.RedirectToAction("Index", "Index");
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult LogOff()
        {
            var ctx = Request.GetOwinContext();
            var authenticationManager = ctx.Authentication;
            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Index");
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> SignUp(SignUpModel model)
        {
            try
            {
                await Auth.SignUp(model);
                //ModelState.AddModelError(string.Empty, "Verifica en tu correo electrónico.");
                ModelState.AddModelError("SUCCESS", "Verifica tu correo electronico");
            }
            catch (Exception ex)
            {
                //ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View();
        }
    }
}