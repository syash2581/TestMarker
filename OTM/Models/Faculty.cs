using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OTM.Models
{
    public class Faculty 
    {
        [Key]
        [Display(Name = "Faculty Roll No")]
        public int Id { get; set; }
        
        [Display(Name = "Faculty Roll No")]
        public string rollno { get; set; }
        [Display(Name = "Faculty Name")]
        public string name { get; set; }
        
        public string type { get; set; } = "Faculty";
        [Display(Name = "Contact Number")]
        public double mno { get; set; }
        [Display(Name = "Email")]
        [EmailAddress]
        public string email { get; set; }
        [DataType(DataType.Password)]
        public string password { get; set; }
        [Display(Name = "Is verified?")]
        public int regornot { get; set; } = 0;

        public int DepartmentId{get; set;}

        public string DeptSname { get; set; }
        public Department department { get; set; }

        public IList<Test> tests { get; set; } 

        
    }
}
