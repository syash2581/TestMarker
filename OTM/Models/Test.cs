using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OTM.Models
{
    public class Test
    {
        [Key]
        public int Id { get; set; }
        [Display(Name="Test Name")]
        public string Name { get; set; }
        [Display(Name = "Test Description")]
        public string Description { get; set; }
        [Display(Name = "Total Number of questions")]
        public int TotalQuestions { get; set; }
        [Display(Name = "Test Total Marks")]
        public int Totalmarks { get; set; }
        [Display(Name = "Test Start Date")]
        public DateTime Testdate { get; set; }
        [Display(Name = "Test Duration ")]
        public DateTime Testduration { get; set; }
        [Display(Name = "Test End Time")]
        public DateTime TestEndtime { get; set; }


        public int FacultyId { get; set; }
        public Faculty Faculty{ get; set; }

        [Display(Name = "Test for sem ")]
        public int SemesterId { get; set; }

        public Semester semester { get; set; }
        public IList<Question> questions { get; set; }

        public IList<StudentTest> studentTests{ get; set; }
    }
}
