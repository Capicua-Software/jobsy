using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace ENTITY_L.Models.Jobs
{
    [FirestoreData]
    public class JobsModel
    {
        [DataType(DataType.Text)]
        [FirestoreProperty]
        public string Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [FirestoreProperty]
        public string Company { get; set; }

        [Required]
        [FirestoreProperty]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [FirestoreProperty]
        public string JobType { get; set; }

        [FirestoreProperty]
        [DataType(DataType.ImageUrl)]
        public string Logo { get; set; }

        [DataType(DataType.Url)]
        [FirestoreProperty]
        public string URL { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [FirestoreProperty]
        public string Job { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [FirestoreProperty]
        public string Location { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [FirestoreProperty]
        public string Category { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [FirestoreProperty]
        public string JobDescription { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [FirestoreProperty]
        public string Requirements { get; set; }

        [DataType(DataType.DateTime)]
        [FirestoreProperty]
        public DateTime date { get; set; }

        [DataType(DataType.Text)]
        [FirestoreProperty]
        public string Date { get; set; }

    }
}