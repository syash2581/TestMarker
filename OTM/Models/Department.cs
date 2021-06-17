using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OTM.Models
{
    public class Department
    {
        [Key]
        [Display(Name = "Department ID")]
        public int Id { get; set; }
        [Display(Name = "Department Code")]
        public int Deptcode { get; set; }
        [Display(Name = "Department Short Name")]
        public string DeptSname { get; set; }

        [Display(Name = "Department Name")]
        public string Deptname { get; set; }

        public IList<Student> student { get; set; }
        public IList<Faculty> Faculty { get; set; }
    }
}
