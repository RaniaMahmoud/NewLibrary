using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Author
    {
        public int ID { get; set; }

        [Required]
        [RegularExpression("[A-Za-z]*", ErrorMessage = "Invalid Name ")]
        [Display(Name = "Author Name")]

        public string Name { get; set; }

        public string SName { get; set; }

        [ForeignKey("Book")]
        public Nullable<int> BookID { get; set; }
        public virtual Book Book { get; set; }
    }
}
