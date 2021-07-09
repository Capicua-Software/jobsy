using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using ENTITY_L.Models.Employer;
using DATA_L.AuthenticationD;
using Firebase.Storage;
using System.IO;
using Firebase.Auth;
using System.Security.Claims;
using ENTITY_L.Models.Jobs;
using ENTITY_L.Models.Request;
using DATA_L.JobsD;

namespace DATA_L.EmployerD
{
    public class EmployerD: FirebaseCore
    {
        static string Default = "https://firebasestorage.googleapis.com/v0/b/jobsy-e4cf0.appspot.com/o/avatar-default.png?alt=media&token=04c01539-770b-4e7f-b883-13b28d963494";
        Jobs Jobs = new Jobs();
        public async Task<List<EmployerModel>> LoadEmployerAsync()
        {
            OpenFirestoreConnection(); // Establece la conexión
            List<EmployerModel> lstemployer = new List<EmployerModel>();
            Query allemployerQuery = db.Collection("Employers"); // Consulta que toma todas las collecciones en la base de datos
            QuerySnapshot allemployerQuerySnapshot = await allemployerQuery.GetSnapshotAsync(); // Ejecuta la consulta

            foreach (DocumentSnapshot documentSnapshot in allemployerQuerySnapshot.Documents) //Recorremos el resultado de la consulta y lo añadimos a la lista
            {
                if (documentSnapshot.Exists) // Si el documento existe
                {
                    EmployerModel employer = documentSnapshot.ConvertTo<EmployerModel>(); // Creamos un nuevo objeto que sera igual al resultado del query
                    employer.Id = documentSnapshot.Id;
                    lstemployer.Add(employer); // Se agrega a la lista el objeto               
                }
            }
            return lstemployer;

        }

        public async Task<EmployerModel> DeleteEmployer(string id)
        {
            OpenFirestoreConnection();
            docRef = db.Collection("Employers").Document(id);
            await docRef.DeleteAsync();

            return null;
        }

        public async Task<EmployerModel> LoadEmployer(string id)
        {

            OpenFirestoreConnection(); // Establece la conexión
            try
            {
                docRef = db.Collection("Employers").Document(id);
                DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

                if (snapshot.Exists)
                {
                    EmployerModel Employer = snapshot.ConvertTo<EmployerModel>();
                    Employer.Id = snapshot.Id;
                    return Employer;
                }
                else
                {
                    return new EmployerModel();
                }
            }
            catch
            {
                throw;
            }

        }

        public async Task<EmployerModel> EditEmployer(EmployerModel model)
        {
            OpenFirestoreConnection();

            docRef = db.Collection("Employers").Document(model.Id);
            EmployerModel search = await LoadEmployer(model.Id);

            if (model.Logo != null) model.Logo = await SaveImage(model.Id, model.Logo);
            else model.Logo = search.Logo;

            if (search.Logo == null) model.Logo = Default;


            Dictionary<string, object> update = new Dictionary<string, object>
                {
                { "Id", model.Id },
                { "Company", model.Company },
                { "Name", model.Company },
                { "URL", model.URL },
                { "RNC", model.RNC },
                { "Tel", model.Tel },
                { "Cel", model.Cel },
                { "Location", model.Location },
                { "Bio", model.Bio },
                { "Instagram", model.Instagram },
                { "Facebook", model.Facebook },
                { "Linkedin", model.Linkedin },
                { "Chips", model.Chips },
                { "Logo", model.Logo },
                { "valido", model.valido }
                };


            await docRef.UpdateAsync(update);
            ENTITY_L.Models.Employer.EmployerModel.image = model.Logo;
            ENTITY_L.Models.Employer.EmployerModel.NameC = model.Company;
            await Jobs.EditjobLogo(model.Logo, model.Company);
            return model; // Retorna el modelo

        }


        public async Task<string> SaveImage(string ID, string route)
        {
            OpenFirestoreConnection();
            // Buscar la imagen en la carpeta upload del proyecto
            var path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Uploads\" + route);
            // Abrir la imagen
            var stream = File.Open(path, FileMode.Open);
            string Email = ClaimsPrincipal.Current.FindFirst(ClaimTypes.Email).Value;

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

        public async Task<List<JobsModel>> employerjobs()
        {
            OpenFirestoreConnection();
            string Email = ClaimsPrincipal.Current.FindFirst(ClaimTypes.Email).Value;
            Query Query = db.Collection("Jobs").WhereEqualTo("Email", Email);
            QuerySnapshot QuerySnapshot = await Query.GetSnapshotAsync();
            List<JobsModel> jobsfound = new List<JobsModel>();
            foreach (DocumentSnapshot documentSnapshot in QuerySnapshot.Documents)
            {
                if (documentSnapshot.Exists) // Si el documento existe
                {
                    JobsModel everyJob = documentSnapshot.ConvertTo<JobsModel>();
                    everyJob.Id = documentSnapshot.Id;
                    jobsfound.Add(everyJob); // Se agrega a la lista el objeto
                }
            }
            return jobsfound;
        }


        public async Task<List<RequestModel>> Loadrequest(string Email) // Método para cargar todos los Empleos
        {
            OpenFirestoreConnection();
            Query query = db.Collection("Solicitudes").WhereEqualTo("EmailCompany", Email);
            List<RequestModel> lstJobs = new List<RequestModel>();
            //Dictionary<string, object> req = null;
            QuerySnapshot requestSnapshot = await query.GetSnapshotAsync();

            foreach (DocumentSnapshot documentSnapshot in requestSnapshot.Documents) //Recorremos el resultado de la consulta y lo añadimos a la lista
            {
                if (documentSnapshot.Exists) // Si el documento existe
                {
                    RequestModel job = documentSnapshot.ConvertTo<RequestModel>();
                    job.JobId = documentSnapshot.Id;
                    lstJobs.Add(job);

                }
            }
            return lstJobs;
        }

        public void DeleteRequest(string id)
        {
            OpenFirestoreConnection(); // Establece la conexión
            try
            {
                DocumentReference empRef = db.Collection("Solicitudes").Document(id);
                empRef.DeleteAsync();
            }
            catch
            {
                throw;
            }
        }

        public async void ChangeRequestStatus(string id, string status)
        {
            OpenFirestoreConnection(); // Establece la conexión
            try
            {
                docRef = db.Collection("Solicitudes").Document(id);

                Dictionary<string, object> update = new Dictionary<string, object>
                {
                { "Estatus", status }
                };

                await docRef.UpdateAsync(update);

            }
            catch
            {
                throw;
            }
        }
    }
}
