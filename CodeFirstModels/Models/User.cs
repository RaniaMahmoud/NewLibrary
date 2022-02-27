using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstModels
{
    public class User
    {
        //public User()
        // {
        //     this.Books = new HashSet<Book>();
        // }
        public int ID { get; set; }
        //[Required]
        [RegularExpression("[A-Za-z]*", ErrorMessage = "Invalid Name ")]
        [Display(Name ="User Name")]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}
