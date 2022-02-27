using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Library.Interfaces
{
    public interface Image
    {
        string photoPath { get; set; }
        HttpPostedFileBase photo { get; set; }
    }
}