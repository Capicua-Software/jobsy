using DATA_L.AuthenticationD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY_L.Models.Jobs;
using Google.Cloud.Firestore;
using Newtonsoft.Json;
using Firebase.Storage;
using System.IO;
using Firebase.Auth;

namespace DATA_L.CategoryD
{
    public class CategoryD:FirebaseCore
    {
        public async Task<CategoryModel> PostCategoryAsync(CategoryModel model)  
        {
            OpenFirestoreConnection(); 
            try
            {
                docRef = db.Collection("Category").Document(); 

                if (model.Logo != null) model.Logo = await SaveImage(docRef.Id, model.Logo);
                else model.Logo = "https://firebasestorage.googleapis.com/v0/b/jobsy-e4cf0.appspot.com/o/Jobs%2Fjobdefault.png?alt=media&token=95eb6412-f9df-4ce1-ad2b-9ed878923b8a";

                model.Date = DateTime.Now.ToString("dd/MM/yyyy");


                Dictionary<string, object> category = new Dictionary<string, object> 
            {
                { "Company", model.Company },
                { "JobType", model.JobType },
                { "Logo", model.Logo },
                { "URL", model.URL },
                { "Job", model.Job },
                { "Location", model.Location },
                { "Category", model.Category },
                { "JobDescription", model.JobDescription },
                { "Requirements", model.Requirements },
                { "Email", model.Email },
                { "Date", model.Date }
            };


                await docRef.SetAsync(category); 
              
                return model; 
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message); 
                throw;
            }

        }

        public async Task<List<CategoryModel>> LoadCategoryAsync()
        {
            OpenFirestoreConnection(); 
            List<CategoryModel> lstCategory = new List<CategoryModel>();
            Query allCategoryQuery = db.Collection("Category"); 
            QuerySnapshot allCategoryQuerySnapshot = await allCategoryQuery.GetSnapshotAsync(); 

            foreach (DocumentSnapshot documentSnapshot in allCategoryQuerySnapshot.Documents) 
            {
                if (documentSnapshot.Exists) 
                {
                    CategoryModel category = documentSnapshot.ConvertTo<CategoryModel>(); 
                    category.Fecha = DateTime.ParseExact(category.Date, "dd/MM/yyyy", null); 
                    lstCategory.Add(category);              
                }
            }
            return lstCategory;
        }

       /* public async Task<IEnumerable<CategoryModel>> GetLastCategoryAsync(int index) // Método para cargar en inicio los N ultimos empleos
        {
            List<CategoryModel> category = await LoadCategoryAsync(); 
            var category = category.OrderByDescending(x => x.Fecha).Take(5);  
            return category; 
*/

        public async Task<CategoryModel> Loadcategory(string id) // * from OpenFirestoreConnection
        {
            OpenFirestoreConnection(); 
            try
            {
                DocumentReference docRef = db.Collection("Category").Document(id);
                DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

                if (snapshot.Exists)
                {
                    CategoryModel category = snapshot.ConvertTo<CategoryModel>();
                    category.Id = snapshot.Id;
                    return category;
                }
                else
                {
                    return new CategoryModel();
                }
            }
            catch
            {
                throw;
            }

        }

        public async Task<CategoryModel> Editcategory(CategoryModel category) 
        {
            OpenFirestoreConnection();
            try
            {
                DocumentReference categoryRef = db.Collection("Category").Document(category.Id);
                var _category = await categoryRef.SetAsync(category, SetOptions.Overwrite);
                return category;
            }
            catch
            {
                throw;
            }
        }


        public void Deletecategory(string id)
        {
            OpenFirestoreConnection(); 
            try
            {
                DocumentReference empRef = db.Collection("Category").Document(id);
                empRef.DeleteAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<CategoryModel>> Searchcategory(string key)
        {
            string keyword = key.ToLower();
            OpenFirestoreConnection();
            List<CategoryModel> category = await LoadCategoryAsync(); 
            List<CategoryModel> categoryfound =new List<CAtegoryModel>();

            foreach (CategoryModel item in category) 
            {
                
                if (string.Equals(keyword, item.Category.ToLower()) || string.Equals(keyword, item.Company.ToLower()) || string.Equals(keyword, item.Category.ToLower()) || string.Equals(keyword, item.Location.ToLower()))
                {
                    categoryfound.Add(item); 
                }
            }
            return categoryfound; 
        }

        public async Task<List<CategoryModel>> Searchbycategory(string keyword)
        {
            OpenFirestoreConnection();
            Query Query = db.Collection("Category").WhereEqualTo("Category", keyword);
            QuerySnapshot QuerySnapshot = await Query.GetSnapshotAsync();
            List<JobsModel> categoryfound = new List<CategoryModel>();
            foreach (DocumentSnapshot documentSnapshot in QuerySnapshot.Documents)
            {
                if (documentSnapshot.Exists) 
                {
                    CategoryModel everyCategory = documentSnapshot.ConvertTo<CategoryModel>();                   
                    categoryfound.Add(everyCategory);
                }
            }
            return categoryfound;
        }

    }
}