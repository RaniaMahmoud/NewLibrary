using CodeFirstModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CodeFirstModels
{
    public class Book
    {
        //public Book()
        //{
        //    this.Users = new HashSet<User>();
        //}
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [RegularExpression("[1-9][0-9]*", ErrorMessage = "Invalid ISBN ")]
        public int ISBN { get; set; }
        [Required]
        [RegularExpression("[1-9][0-9]*", ErrorMessage = "Invalid Yeare ")]
        public int Yeare { get; set; }
        [Required]
        [Display(Name ="Author Name")]
        public string AuthorName { get; set; }

        public string ImagePath { get; set; }

        [NotMapped]
        public HttpPostedFileBase Image { get; set; }
        [ForeignKey("User")]
        public Nullable<int> UserID { get; set; }
        public virtual User User { get; set; }
        [ForeignKey("Department")]
        public int DepartmentID { get; set; }
        public virtual Department Department { get; set; }
        [ForeignKey("Publisher")]
        public int PublisherID { get; set; }
        public virtual Publisher Publisher { get; set; }

        public virtual ICollection<Author> Authors { get; set; }
    }
}
