using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace ENTITY_L.Models.User
{
    [FirestoreData]
    public class UserModel
    {
        [DataType(DataType.Text)]
        [FirestoreProperty]       
        public string Id { get; set; }

        [DataType(DataType.Text)]
        [FirestoreProperty]
        public string Name { get; set; }

        [DataType(DataType.Text)]
        [FirestoreProperty]
        public string LastName { get; set; }

        [DataType(DataType.Text)]
        [FirestoreProperty]
        public string Email { get; set; }

        [DataType(DataType.Text)]
        [FirestoreProperty]
        public string Cedula { get; set; }


        [DataType(DataType.Text)]
        [FirestoreProperty]
        public string Tel { get; set; }

        [DataType(DataType.Text)]
        [FirestoreProperty]
        public string Cel { get; set; }


        [DataType(DataType.Text)]
        [FirestoreProperty]
        public string Direccion { get; set; }

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


        [DataType(DataType.Text)]
        [FirestoreProperty]
        public string[] Chips { get; set; }

        [FirestoreProperty]
        [DataType(DataType.Text)]
        public string Image { get; set; }

    }
}
