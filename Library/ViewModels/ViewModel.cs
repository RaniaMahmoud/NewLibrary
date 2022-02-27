using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeFirstContext;
using System.Web.Mvc;
using Library.Models;

namespace Library.ViewModels
{
    public class ViewModel
    {

        public Book Book { get; set; }
        public IEnumerable<SelectListItem> Departments { get; set; }
        
        public IEnumerable<SelectListItem> Publishers { get; set; }

    }
}