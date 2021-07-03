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
using System.Security.Claims;
using ENTITY_L.Models.Limite;

namespace DATA_L.AdminD
{
    public class AdminD:FirebaseCore
    {

        public async Task<string> getlimit()
        {
            OpenFirestoreConnection(); // Establece la conexión
            Query allJobsQuery = db.Collection("Settings"); // Consulta que toma todas las collecciones en la base de datos
            QuerySnapshot allJobsQuerySnapshot = await allJobsQuery.GetSnapshotAsync(); // Ejecuta la consulta
            string limit = null;
            foreach (DocumentSnapshot documentSnapshot in allJobsQuerySnapshot.Documents) //Recorremos el resultado de la consulta y lo añadimos a la lista
            {
                if (documentSnapshot.Exists) // Si el documento existe
                {
                    LimiteModel lim = documentSnapshot.ConvertTo<LimiteModel>(); // Creamos un nuevo objeto que sera igual al resultado del query
                    limit = lim.lim;       
                }
            }
            return limit;
        }

        public async Task<LimiteModel> editlimit (LimiteModel model)
        {
            docRef = db.Collection("Settings").Document("limite");
            Dictionary<string, object> update = new Dictionary<string, object>
                {
                { "lim", model.lim}
                };
            await docRef.UpdateAsync(update);

            return model ;
        }

    }
}
