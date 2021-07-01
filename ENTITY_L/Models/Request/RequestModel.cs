using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace ENTITY_L.Models.Request
{

    [FirestoreData]
    public class RequestModel
    {
        [DataType(DataType.Text)]
        [FirestoreProperty]
        public string IdJob { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [FirestoreProperty]
        public string CedulaUser { get; set; }

        [Required]
        [FirestoreProperty]
        public string EmailUser { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [FirestoreProperty]
        public string EmailCompany { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [FirestoreProperty]
        public string Job { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [FirestoreProperty]
        public string JobType { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [FirestoreProperty]
        public string JobDescription { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [FirestoreProperty]
        public string Message { get; set; }


        [DataType(DataType.Text)]
        [FirestoreProperty] 
        public string Estatus { get; set; }

        [DataType(DataType.Text)]
        [FirestoreProperty]
        public string Date { get; set; }

    }
}
