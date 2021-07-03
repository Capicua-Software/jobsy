using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DATA_L.Category;
using ENTITY_L.Models.Category;

namespace Jobsy_API.Controllers
{
    public class CategoryAPIController : ApiController
    {
        CategoryD category = new CategoryD();
     
        [HttpPost]
        [Route("api/Category/PostCategory")]
        public Task<CategoryModel> AddCategory(CategoryModel model)
        {
            return category.AddCategory(model); 
        }

        [HttpPost]
        [Route("api/Category/DeleteCategory")]
        public void DeleteCategory(string id)
        {
            category.DeleteCategory(id);
        }

        [HttpGet]
        [Route("api/Category/LoadCategoryAsync")]
        public async Task<List<CategoryModel>> LoadCategoryAsync()
        {
            List<CategoryModel> lstcategory = await category.LoadCategoryAsync(); // Se guarda en una lista el resultado del metodo
            return lstcategory; //Retorna una lista     
        }

        [HttpPost]
        [Route("api/Category/EditCategory")]
        public async Task<CategoryModel> EditCategory(CategoryModel model)
        {
            var _category = await category.EditCategory(model);
            return _category;
        }

        [HttpGet]
        [Route("api/Category/LoadCategory")]  // Ruta de la API
        public async Task<CategoryModel> LoadCategory(string id)
        {
            CategoryModel categr = await category.LoadCategory(id);
            return categr; //Retorna una lista 
        }

       

    }
}
