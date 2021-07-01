using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ENTITY_L.Models.Request;
using Jobsy_API.Controllers;
using System.Threading.Tasks;

namespace Jobsy.Controllers
{
    public class RequestController : Controller
    {
        RequestAPIController request = new RequestAPIController();
        // GET: Request
        public ActionResult Index()
        {
            return RedirectToAction("LoadRequest");
        }
        public ActionResult TableRequestUser()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> SendRequest(RequestModel model) // Este metodo se llama al enviar el formulario
        {


            await request.SendRequest(model); // Llama al metodo que se encuenta en la API

            return View();//Retorna la vista
        }



        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> LoadRequest(string id) // Este metodo se llama al enviar el formulario
        {

            id = "CZ4WsQWPWlGOC5C4Sbt8";
            ViewBag.request = await request.LoadRequest(id);

            return View("LoadRequest");
        }


        

    }
}