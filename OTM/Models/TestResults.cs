using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OTM.Models
{
    public class TestResults
    {
        [Key]
        public int key { get; set; }
        public string rollno { get; set; }
        public string sname { get; set; }
        public int marks { get; set; }
    }
}
