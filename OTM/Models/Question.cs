using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OTM.Models
{
    public class Question
    {
        [Key]
        [Display(Name = "Question ID")]
        public int Id { get; set; }
        [Display(Name = "Question string ")]
        public string question { get; set; }
        [Display(Name = "Brief Introduction")]
        public string QuestionBrief { get; set; }
        [Display(Name =" Question marks ")]
        public int Marks { get; set; }

        public int TestId { get; set; }
        public Test Test { get; set; }

        public IList<Options> Options { get; set; }
    }
}
