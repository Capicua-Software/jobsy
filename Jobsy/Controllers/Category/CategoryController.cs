using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ENTITY_L.Models.Category;
using Jobsy_API.Controllers;
using Microsoft.Owin.Security;
using System.IO;

namespace Jobsy.Controllers
{
    public class CategoryController : Controller
    {
        CategoryAPIController category = new CategoryAPIController();
        // GET: Category
        public ActionResult PostCategory()
        {
            return View();
        }

        public ActionResult EditCategory()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> PostCategory(CategoryModel model) // Este metodo se llama al enviar el formulario
        {
            try
            {              
                await category.AddCategory(model); // Llama al metodo que se encuenta en la API
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);//Lanza un mensaje en la consola en caso de error
            }

            return RedirectToAction("LoadCategoryAsync");
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> LoadCategoryAsync() // Metodo para devolver una vista con todos los empleaos 
        {
            try
            {
               
                ViewBag.AllCategory = await LoadCategories(); //Guardamos el resultado del metodo en el Viewbag
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message); //Lanza un mensaje en la consola en caso de error
            }

            return View(); //Retorna la vista
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<List<CategoryModel>> LoadCategories() // Metodo para devolver una vista con todos los empleaos 
        {
            try
            {
                List<CategoryModel> Category = await category.LoadCategoryAsync(); // Llama al metodo que se encuenta en la API
                return Category;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message); //Lanza un mensaje en la consola en caso de error
            }

            return null;
        }




        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> EditCategory(string id)
        {
            try
            {
                CategoryModel acategory = await category.LoadCategory(id);
                ViewBag.acategory = acategory;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return View("EditCategory");
        }




        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Edit(CategoryModel Categorytoedit)
        {
            try
            {
                await category.EditCategory(Categorytoedit);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return RedirectToAction("LoadCategoryAsync");
        }



        [HttpGet]
        [AllowAnonymous]
        public ActionResult DeleteCategory(string id)
        {
            try
            {
                category.DeleteCategory(id);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return RedirectToAction("LoadCategoryAsync");
        }

    }
}