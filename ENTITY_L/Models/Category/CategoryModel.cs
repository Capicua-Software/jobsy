using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;


namespace ENTITY_L.Models.Category
{
    [FirestoreData]
    public class CategoryModel
    {
        [DataType(DataType.Text)]
        [FirestoreProperty]
        public string Id { get; set; }

        [DataType(DataType.Text)]
        [FirestoreProperty]
        public string Name { get; set; }

    }
}
