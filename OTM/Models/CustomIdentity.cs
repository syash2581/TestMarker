using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace OTM.Models
{
    public class CustomIdentity : IdentityUser
    {
        [Display(Name = "Roll No")]
        [Required]
        public string rollno { get; set; }
        [Display(Name = "Name")]
        [Required]
        public string name { get; set; }
        [Display(Name ="Register or login as")]
        [Required]
        public string type { get; set; } 
        [Display(Name = "Contact Number")]
        [Required]
        [RegularExpression(@"^[6-9]{1}[0-9]{9}$", ErrorMessage ="Please Enter Valid Mobile Number")]
        public double mno { get; set; }
        [Display(Name = "Email")]
        [Required]
        [EmailAddress]
        public override string Email{ get; set; }
        [Display(Name = "Is verified?")]
        public int regornot { get; set; } = 0;


        
        
        public int DepartmentId { get; set; }
        
        public Department department { get; set; }
        [Display(Name = "Department")]
        [Required]
        public string DeptSname { get; set; }
        [Display(Name = "Semester")]
        public int SemesterId { get; set; }
        public Semester semester { get; set; }
        public IList<StudentTest> studentTests { get; set; }
        [DataType(DataType.Password)]
        [Required]
        [MinLength(10, ErrorMessage = "Enter Password of Minimum 10 Digits")]
        //[RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only Alphabets and Numbers are allowed.")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public string ReturnUrl { get; set; }
    }
}
