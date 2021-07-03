using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Auth;


namespace ENTITY_L.Models.Authentication
{
    public class LoginModel
    {
        [Required (ErrorMessage = "Este campo es requerido")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        
        public string Password { get; set; }
        public string Cedula { get; set; }
        public string Logo { get; set; }
        public string State { get; set; }
        public string Role { get; set; }
        public string UserName { get; set; }
        public FirebaseAuthProvider auth { get; set; }
        public FirebaseAuthLink ab { get; set; }
        public string token { get; set; }
        public Firebase.Auth.User user { get; set; }
        
    }
}
