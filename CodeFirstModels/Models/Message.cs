using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstModels
{
    public class Message
    {
        public int ID { get; set; }
        [Required]
        [RegularExpression("[A-Za-z]*", ErrorMessage = "Invalid")]

        [StringLength(100)]
        [DataType(DataType.Text)]
        public string Text { get; set; }
        //public DateTime time { get; set; }
        [ForeignKey("User")]
        public Nullable<int> UserID { get; set; }
        public virtual User User { get; set; }

    }
}
