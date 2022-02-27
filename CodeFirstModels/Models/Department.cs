using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstModels
{
    [Table("Departments", Schema = "LibraryE")]

    public class Department
    {
        public int ID { get; set; }
        [Required]
        [RegularExpression("[A-Za-z]*", ErrorMessage = "Invalid Name ")]
        [Display(Name = "Department Name")]
        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }


    }
}
