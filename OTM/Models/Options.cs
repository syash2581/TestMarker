using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OTM.Models
{
    public class Options
    {
        [Key]
        [Display(Name = "Option Id")]
        public int Id { get; set; }
        [Display(Name = "Option Value")]
        public string Option { get; set; }
        [Display(Name = "Option Description")]
        //correct or not ; if correct why and if not then why ?
        public string Description { get; set; }
        [Display(Name = "Is Answer Correct ?")]
        public bool Correct { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
