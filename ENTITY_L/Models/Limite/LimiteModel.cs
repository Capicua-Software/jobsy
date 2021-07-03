using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace ENTITY_L.Models.Limite
{
    [FirestoreData]
    public class LimiteModel
    {
        [Required]
        [DataType(DataType.Text)]
        [FirestoreProperty]
        public string lim { get; set; }
    }
}
