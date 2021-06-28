using DATA_L.AuthenticationD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY_L.Models.Jobs;
using Google.Cloud.Firestore;
using ENTITY_L.Models.Category;


namespace DATA_L.Category
{

    public class CategoryD: FirebaseCore
    {

        public async Task<CategoryModel> AddCategory(CategoryModel Model)
        {
            OpenFirestoreConnection();
            docRef = db.Collection("Category").Document();

            Dictionary<string, object> job = new Dictionary<string, object> //Diccionario de datos con los campos y sus respectivos valores
            {              
                { "Name", Model.Name }
            };
            
            await docRef.SetAsync(job);
            return Model;
        }

        public void DeleteCategory(string id)
        {
            OpenFirestoreConnection(); // Establece la conexión
            try
            {
                docRef = db.Collection("Category").Document(id);
                docRef.DeleteAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<CategoryModel> EditCategory(CategoryModel Model) 
        {
           
            try
            {
                OpenFirestoreConnection(); // Establece la conexión
                docRef = db.Collection("Category").Document(Model.Id);                
                
                Dictionary<string, object> update = new Dictionary<string, object> //Diccionario de datos con los campos y sus respectivos valores
                {    
                { "Name", Model.Name }
                };

                await docRef.UpdateAsync(update);
                return Model;

            }
            catch
            {
                throw;
            }
        }

        public async Task<List<CategoryModel>> LoadCategoryAsync()
        {
            OpenFirestoreConnection(); // Establece la conexión
            List<CategoryModel> lstCategory = new List<CategoryModel>();
            Query allCategoryQuery = db.Collection("Category"); // Consulta que toma todas las collecciones en la base de datos
            QuerySnapshot allCategoryQuerySnapshot = await allCategoryQuery.GetSnapshotAsync(); // Ejecuta la consulta

            foreach (DocumentSnapshot documentSnapshot in allCategoryQuerySnapshot.Documents) //Recorremos el resultado de la consulta y lo añadimos a la lista
            {
                if (documentSnapshot.Exists) // Si el documento existe
                {
                    CategoryModel category = documentSnapshot.ConvertTo<CategoryModel>(); // Creamos un nuevo objeto que sera igual al resultado del query
                    category.Id = documentSnapshot.Id;
                    lstCategory.Add(category); // Se agrega a la lista el objeto               
                }
            }
            return lstCategory;
        }

        public async Task<CategoryModel> LoadCategory(string Id) 
        {
            OpenFirestoreConnection(); // Establece la conexión
            try
            {
                docRef = db.Collection("Category").Document(Id);
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
    }
}
