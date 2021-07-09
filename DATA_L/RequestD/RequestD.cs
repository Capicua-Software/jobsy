using System;
using System.Collections.Generic;
using System.Linq;
using DATA_L.AuthenticationD;
using System.Text;
using System.Threading.Tasks;
using ENTITY_L.Models.Request;
using Google.Cloud.Firestore;
using Newtonsoft.Json;
using Firebase.Auth;
using System.Security.Claims;
using System.Web.Helpers;

namespace DATA_L.Request
{
   public class RequestD: FirebaseCore
    {

        public async Task<RequestModel> SendRequest(RequestModel model)  // Metodo para guardar un empleo en Firestore
        {
            OpenFirestoreConnection(); // Establece la conexión
            try
            {
                docRef = db.Collection("Solicitudes").Document();

                model.Date = DateTime.Now.ToString("dd/MM/yyyy");
                model.Estatus = "En Espera";

                Dictionary<string, object> jobRequest = new Dictionary<string, object> //Diccionario de datos con los campos y sus respectivos valores
                {
                { "CedulaUser", model.CedulaUser },
                { "EmailUser", model.EmailUser },
                { "EmailCompany", model.EmailCompany },
                { "Job", model.Job },
                { "JobType", model.JobType },               
                { "JobDescription", model.JobDescription },
                { "Message", model.Message },
                { "Estatus", model.Estatus },
                { "Date", model.Date }
                };

                /*
                Dictionary<string, object> request = new Dictionary<string, object>
                {
                    {model.CedulaUser,  jobRequest}
                };
                */

                /*
                List<RequestModel> lstReq = new List<RequestModel>();

                List<object> listValues = jobRequest.Values.ToList();
                string json = JsonConvert.SerializeObject(listValues);
                RequestModel newreq = Json.Decode<RequestModel>(json.Substring(1, json.Length - 2));

                Dictionary<string, object> requestOther = new Dictionary<string, object>
                {
                    {model.CedulaUser,  newreq}
                };*/

                await docRef.SetAsync(jobRequest); // Guardar en la colección de Jobs el diccionario

                return model; // Retorna el modelo
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message); // En caso de una excepción imprime más información en la consola
                throw;
            }

        }



        // 1. Buscar el trabajo por Id (El trabajo es un documento)
        // 2. Filtrar por el correo (Coleccion)
        // 3. Retornar los campos.


        //$collection = $firestore->collection('Solicitudes')->document($identificador );
        //$snapshot = $collection->snapshot();
        //$sata = [];

        //if ($snapshot->exists()) {
        //$sata[] = $snapshot->data();
        //} else {
        //printf('Documento %s does not exist!' . PHP_EOL, $snapshot->id());
        //}
        //}



        public async Task<List<RequestModel>> Loadrequest(string Email) // Método para cargar todos los Empleos
        {
            OpenFirestoreConnection();
            Query query = db.Collection("Solicitudes").WhereEqualTo("EmailUser", Email);
            List<RequestModel> lstJobs = new List<RequestModel>();
            Dictionary<string, object> req = null;
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


        public async  Task<RequestModel> DeleteRequest(string IdJob)
        {
            OpenFirestoreConnection(); // Establece la conexión
            try
            {
                var Email = ClaimsPrincipal.Current.FindFirst(ClaimTypes.Email).Value;
                docRef = db.Collection("Solicitudes").Document(IdJob);
                Dictionary<string, object> updates = new Dictionary<string, object>
                {
                { Email, FieldValue.Delete }
                };
                await docRef.UpdateAsync(updates);
            }
            catch
            {
                throw;
            }

            return null;
        }

    }
}
