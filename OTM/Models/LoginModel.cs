using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OTM.Models
{
    public class LoginModel
    {

        [Key]
        [Display(Name ="Roll No")]
        [Required]
        public string rollno { get; set; }

        [Required]
        [Display(Name = "Login As A")]
        public string usertype { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password2 { get; set; }
    }
}
