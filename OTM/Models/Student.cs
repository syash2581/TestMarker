using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace OTM.Models 
{
    
    public class Student 
    {
        [Key]
        public int Id { get; set; }
        
        [Display(Name = "Roll No")]
        public string rollno { get; set; }
        [Display(Name ="Student Name")]
        public string Sname { get; set; }

        [Display(Name = "Mobile Number")]
        
        public double Mno { get; set; }

        [Display(Name = "Student Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        
        public string Password { get; set; }
        public int regornot { get; set; } = 0;
        public int DepartmentId { get; set; }
        public string DeptSname { get; set; }
        public Department department { get; set; }

        public int SemesterId { get; set; }
        public Semester semester { get; set; }
        public IList<StudentTest> studentTests{ get; set; }
    }
}
