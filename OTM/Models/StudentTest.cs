using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OTM.Models
{
    public class StudentTest
    {
        public int StudentId { get; set; }
        public Student student { get; set; }

        public int TestId{get; set;}
        public Test test { get; set; }

        public DateTime attendedDate { get; set; }

        public int results { get; set; }
    }
}
