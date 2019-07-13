using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Indicadores.Models;
using System.Web.Mvc;


namespace Indicadores.Models
{
    public class MenuViewModel
    {
        public List<SelectListItem> MenuMeta{ get; set; }

        public string SiglasMet { get; set; }
    }
}