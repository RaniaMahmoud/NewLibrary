using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstModels
{
    public class Publisher
    {
        public int ID { get; set; }
        [Required]
        [RegularExpression("[A-Za-z]*", ErrorMessage = "Invalid Name ")]
        [Display(Name = "Publisher Name")]

        public string Name { get; set; }

    }
}
