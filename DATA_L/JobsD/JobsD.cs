using DATA_L.AuthenticationD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY_L.Models.Jobs;
using Google.Cloud.Firestore;

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
                { "Compañía", model.Company },
                { "Tipo de Empleo", model.JobType },
                { "Logo", model.Logo },
                { "URL", model.URL },
                { "Posición", model.Job },
                { "Ubicación", model.Location },
                { "Categoría", model.Category },
                { "Descripción del trabajo", model.JobDescription },
                { "Cómo aplicar", model.Requirements },
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
    }
}
