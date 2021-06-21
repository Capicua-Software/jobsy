using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY_L.Models.Jobs
{
    public class JobsModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Company")]
        public string Company { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string JobType { get; set; }

        public string Logo { get; set; }
        public string URL { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Job { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Location { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Category { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string JobDescription { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Requirements { get; set; }

    }
}
