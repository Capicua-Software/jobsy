using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace ENTITY_L.Models.Employer
{

    [FirestoreData]
    public class EmployerModel
    {
        [DataType(DataType.Text)]
        [FirestoreProperty]
        public string Id { get; set; }

        [DataType(DataType.Text)]
        [FirestoreProperty]
        public string Company { get; set; }


        [DataType(DataType.Text)]
        [FirestoreProperty]
        public string RNC { get; set; }

        [DataType(DataType.Text)]
        [FirestoreProperty]
        public string Tel { get; set; }

        [DataType(DataType.Text)]
        [FirestoreProperty]
        public string Cel { get; set; }


        [DataType(DataType.Text)]
        [FirestoreProperty]
        public string Location { get; set; }

        [DataType(DataType.Text)]
        [FirestoreProperty]
        public string Email { get; set; }

        [DataType(DataType.Text)]
        [FirestoreProperty]
        public string Bio { get; set; }

        [DataType(DataType.Url)]
        [FirestoreProperty]
        public string Instagram { get; set; }

        [DataType(DataType.Url)]
        [FirestoreProperty]
        public string Facebook { get; set; }

        [DataType(DataType.Url)]
        [FirestoreProperty]
        public string Linkedin { get; set; }

        [DataType(DataType.Url)]
        [FirestoreProperty]
        public string URL { get; set; }

        [DataType(DataType.Text)]
        [FirestoreProperty]
        public List<string> Chips { get; set; }
        public string chip { get; set; }

        [FirestoreProperty]
        [DataType(DataType.Text)]
        public string Logo { get; set; }
    }
}
