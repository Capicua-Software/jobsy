using DATA_L.AuthenticationD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY_L.Models.Jobs;
using Google.Cloud.Firestore;
using Newtonsoft.Json;

namespace DATA_L.JobsD
{
    public class JobsD:FirebaseCore
    {
        public async Task<JobsModel> PostJobsAsync(JobsModel model)  // Metodo para guardar un empleo en Firestore
        {
            OpenFirestoreConnection(); // Establece la conexión
            try
            {              
                Dictionary<string, object> job = new Dictionary<string, object> //Diccionario de datos con los campos y sus respectivos valores
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

                docRef = await db.Collection("Jobs").AddAsync(job); // Guardar en la colección de Jobs el diccionario
                return model; // Retorna el modelo
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message); // En caso de una excepción imprime más información en la consola
                throw;
            }

        }

        public async Task<List<JobsModel>> LoadJobsAsync()
        {
            OpenFirestoreConnection(); // Establece la conexión
            List<JobsModel> lstJobs = new List<JobsModel>();
            Query allJobsQuery = db.Collection("Jobs"); // Consulta que toma todas las collecciones en la base de datos
            QuerySnapshot allJobsQuerySnapshot = await allJobsQuery.GetSnapshotAsync(); // Ejecuta la consulta

            foreach (DocumentSnapshot documentSnapshot in allJobsQuerySnapshot.Documents) //Recorremos el resultado de la consulta y lo añadimos a la lista
            {
                if (documentSnapshot.Exists) // Si el documento existe
                {
                    JobsModel job = documentSnapshot.ConvertTo<JobsModel>(); // Creamos un nuevo objeto que sera igual al resultado del query
                    job.date = DateTime.ParseExact(job.Date, "dd/MM/yyyy", null); //Se realiza una conversion de la fecha a datetime
                    lstJobs.Add(job); // Se agrega a la lista el objeto               
                }
            }
            return lstJobs;
        }

        public async Task<IEnumerable<JobsModel>> GetLastJobsAsync(int index) // Método para cargar en inicio los N ultimos empleos
        {
            List<JobsModel> jobs = await LoadJobsAsync(); // Se guarda en una lista todos los empleos que se encuentran en la bd  
            var job = jobs.OrderByDescending(x => x.date).Take(index);  // Se ordena la lista por fecha en orden descendiente y se toma N cantidad de empleos
            return job; // Se retorna la lista
        }


        public async Task<JobsModel> Loadjob(string id) // Método para cargar todos los Empleos
        {
            OpenFirestoreConnection(); // Establece la conexión
            try
            {
                DocumentReference docRef = db.Collection("Jobs").Document(id);
                DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

                if (snapshot.Exists)
                {
                    JobsModel job = snapshot.ConvertTo<JobsModel>();
                    job.Id = snapshot.Id;
                    return job;
                }
                else
                {
                    return new JobsModel();
                }
            }
            catch
            {
                throw;
            }

        }

        public async Task<JobsModel> Editjob(JobsModel job) // Método para cargar todos los Empleos
        {
            OpenFirestoreConnection(); // Establece la conexión
            try
            {
                DocumentReference jobRef = db.Collection("Jobs").Document(job.Id);
                var _job = await jobRef.SetAsync(job, SetOptions.Overwrite);
                return job;
            }
            catch
            {
                throw;
            }
        }


        public void Deletejob(string id)
        {
            OpenFirestoreConnection(); // Establece la conexión
            try
            {
                DocumentReference empRef = db.Collection("Jobs").Document(id);
                empRef.DeleteAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<JobsModel>> Searchjob(string key)
        {
            string keyword = key.ToLower();
            OpenFirestoreConnection();
            List<JobsModel> jobs = await LoadJobsAsync(); // Una lista
            List<JobsModel> jobsfound =new List<JobsModel>();

            foreach (JobsModel item in jobs) // Recorremos los datos que se encuentran en la lista
            {
                // Comparamos los campos con lo que ingreso el usuario
                if (string.Equals(keyword, item.Job.ToLower()) || string.Equals(keyword, item.Company.ToLower()) || string.Equals(keyword, item.Category.ToLower()) || string.Equals(keyword, item.Location.ToLower()))
                {
                    jobsfound.Add(item); // Se agrega a la lista el objeto
                }
            }
            return jobsfound; // Retornamos la lista
        }

        public async Task<List<JobsModel>> Searchbycategory(string keyword)
        {
            OpenFirestoreConnection();
            Query Query = db.Collection("Jobs").WhereEqualTo("Category", keyword);
            QuerySnapshot QuerySnapshot = await Query.GetSnapshotAsync();
            List<JobsModel> jobsfound = new List<JobsModel>();
            foreach (DocumentSnapshot documentSnapshot in QuerySnapshot.Documents)
            {
                if (documentSnapshot.Exists) // Si el documento existe
                {
                    JobsModel everyJob = documentSnapshot.ConvertTo<JobsModel>();                   
                    jobsfound.Add(everyJob); // Se agrega a la lista el objeto

                }
            }
            return jobsfound;
        }

    }
}



