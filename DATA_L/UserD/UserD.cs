using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using ENTITY_L.Models.User;
using DATA_L.AuthenticationD;
using Firebase.Storage;
using System.IO;
using Firebase.Auth;
using System.Security.Claims;

namespace DATA_L.UserD
{
    public class UserD: FirebaseCore
    {

        static string Default = "https://firebasestorage.googleapis.com/v0/b/jobsy-e4cf0.appspot.com/o/avatar-default.png?alt=media&token=04c01539-770b-4e7f-b883-13b28d963494";

        public async Task<List<UserModel>> LoadUsersAsync()
        {           
            OpenFirestoreConnection(); // Establece la conexión
            List<UserModel> lstUsers = new List<UserModel>();
            Query alluserdQuery = db.Collection("Users"); // Consulta que toma todas las collecciones en la base de datos
            QuerySnapshot allusersQuerySnapshot = await alluserdQuery.GetSnapshotAsync(); // Ejecuta la consulta

            foreach (DocumentSnapshot documentSnapshot in allusersQuerySnapshot.Documents) //Recorremos el resultado de la consulta y lo añadimos a la lista
            {
                if (documentSnapshot.Exists) // Si el documento existe
                {
                    UserModel user = documentSnapshot.ConvertTo<UserModel>(); // Creamos un nuevo objeto que sera igual al resultado del query
                    user.Id = documentSnapshot.Id;
                    lstUsers.Add(user); // Se agrega a la lista el objeto               
                }
            }
            return lstUsers;
        }

        public async Task<UserModel> DeleteUser(string id)
        {
           OpenFirestoreConnection();
           docRef = db.Collection("Users").Document(id);
           await docRef.DeleteAsync();

           return null;
        }

        public async Task<UserModel> LoadUser(string id)
        {
            
            OpenFirestoreConnection(); // Establece la conexión
            try
            {
                docRef = db.Collection("Users").Document(id);
                DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

                if (snapshot.Exists)
                {
                    UserModel user = snapshot.ConvertTo<UserModel>();
                    user.Id = snapshot.Id;
                    return user;
                }
                else
                {
                    return new UserModel();
                }
            }
            catch
            {
                throw;
            }

        }

        public async Task<UserModel> EditUser(UserModel model)
        {
            OpenFirestoreConnection();

            docRef = db.Collection("Users").Document(model.Id);
            UserModel search = await LoadUser(model.Id);

            if (model.Logo != null) model.Logo = await SaveImage(model.Id, model.Logo);
            else model.Logo = search.Logo;

            if (search.Logo == null) model.Logo = Default;

         
            Dictionary<string, object> update = new Dictionary<string, object>
                {
                { "Id", model.Id },
                { "Name", model.Name },
                { "LastName", model.LastName },
                { "Cedula", model.Cedula },
                { "Tel", model.Tel },
                { "Cel", model.Cel },
                { "Direccion", model.Direccion },
                { "Bio", model.Bio },
                { "Instagram", model.Instagram },
                { "Facebook", model.Facebook },
                { "Linkedin", model.Linkedin },
                { "Chips", model.Chips },
                { "Logo", model.Logo }
                };



            await docRef.UpdateAsync(update);
            return model; // Retorna el modelo

        }


        public async Task<string> SaveImage(string ID, string route)
        {
            OpenFirestoreConnection();
            // Buscar la imagen en la carpeta upload del proyecto
            var Email = ClaimsPrincipal.Current.FindFirst(ClaimTypes.Email).Value;
            var path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Uploads\" + route);
            // Abrir la imagen
            var stream = File.Open(path, FileMode.Open);


            //autenticancion
            var auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
            var a = await auth.SignInWithEmailAndPasswordAsync("capicuasoftware@gmail.com", "capicuasoftware");

            // Constructor FirebaseStorage, define la ruta de FirebaseStorage donde se guardara el archivo
            var task = new FirebaseStorage(
                Bucket,

                 new FirebaseStorageOptions
                 {
                     AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                     ThrowOnCancel = true,
                 })
                .Child(Email)
                .Child("Profile")
                .Child(ID)
                .PutAsync(stream);


            var downloadUrl = await task;

            return downloadUrl; //Retorna el link de descarga de la foto

        }

        public async Task<UserModel> GetRequestedJobs(string cedula)
        {
            OpenFirestoreConnection();
            Query query = db.Collection("Solicitudes").WhereEqualTo($"{cedula}.CedulaUser", cedula);
            QuerySnapshot querySnapshot = await query.GetSnapshotAsync();
            var queryTest = querySnapshot;
            UserModel user = queryTest;
            string hi = "";
            
            return user;
    


        }
    }
}
