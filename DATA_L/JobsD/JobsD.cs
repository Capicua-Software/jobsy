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
                { "Email", model.Email }
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


        public async Task<List<JobsModel>> LoadJobsAsync() // Método para cargar todos los Empleos
        {
            OpenFirestoreConnection(); // Establece la conexión
            Query allJobsQuery = db.Collection("Jobs"); // Consulta que toma todas las collecciones en la base de datos
            QuerySnapshot allJobsQuerySnapshot = await allJobsQuery.GetSnapshotAsync(); // Ehecuta la consulta
            List<JobsModel> lstJobs = new List<JobsModel>(); // Declaramos una lista para guardar los datos 
            foreach (DocumentSnapshot documentSnapshot in allJobsQuerySnapshot.Documents) //Recorremos el resultado de la consulta y lo añadimos a la lista
            {
                if (documentSnapshot.Exists) // Si el documento existe
                {
                    Dictionary<string, object> _jobs = documentSnapshot.ToDictionary(); // Se guarda el resultado en un diccionario de datos
                  
                    string json = JsonConvert.SerializeObject(_jobs); // Se conviere a jsopon el resultado
                    JobsModel newJob = JsonConvert.DeserializeObject<JobsModel>(json); // Se crea un objeto que es igual a ese Json Deserializado 
                    newJob.Id = documentSnapshot.Id; // Se guarda el ID del documento en una parte de la lista
                    lstJobs.Add(newJob); // Se agrega a la lista el objeto

                }
            }
            
            return lstJobs; // Retorna la lista

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
    }
}
