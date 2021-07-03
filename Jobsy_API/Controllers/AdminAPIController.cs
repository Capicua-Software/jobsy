using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DATA_L.AdminD;
using System.Threading.Tasks;
using ENTITY_L.Models.Limite;

namespace Jobsy_API.Controllers
{
    public class AdminAPIController : Controller
    {
        AdminD admin = new AdminD();

        [HttpGet]
        [Route("api/Jobs/getlimit")]  // Ruta de la API
        public async Task<string> getlimit()
        {
            string limite = await admin.getlimit(); 
            return limite;
        }

        [HttpGet]
        [Route("api/Jobs/editlimit")]  // Ruta de la API
        public async Task<string> editlimit(LimiteModel edit)
        {
            await admin.editlimit(edit);
            return null;
        }
    }
}