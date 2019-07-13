using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Indicadores.Controllers
{
    public class HomeController : Controller
    {
        // GET: Index
        public ActionResult Index()
        {
            return View();
        }

        // GET: About
        public ActionResult About()
        {
            ViewBag.Message = "Sucursal Electronica";

            return View();
        }

        // GET: Contact
        public ActionResult Contact()
        {
            ViewBag.Message = "Informacion de contacto";

            return View();
        }

        // GET: Mantenimiento
        public ActionResult Mantenimiento()
        {
            ViewBag.Message = "Mantenimiento";
            return View();
        }
    }
}