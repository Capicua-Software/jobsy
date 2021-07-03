using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY_L.Models.Authentication
{
    public class SignUpModel
    {

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public string Cedula { get; set; } = "";

        public bool Employer { get; set; }
        public string Role { get; set; }

        public FirebaseAuthProvider auth { get; set; }
        public FirebaseAuthLink a { get; set; }


    }
}