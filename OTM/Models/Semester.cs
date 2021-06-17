using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OTM.Models
{
    public class Semester
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Sem")]
        public int sem { get; set; }

        public IList<Student> students { get; set; }


    }
}
