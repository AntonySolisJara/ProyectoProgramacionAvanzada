using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Indicadores.Models;

namespace Indicadores.Controllers
{
    public class CuentaCatalogoController : Controller
    {
        private CuentaCatalogo cuenta = new CuentaCatalogo();
        // GET: CuentaCatalogo
        public ActionResult Index()
        {
            return View(cuenta.ListarCuenta());
        }

        // GET: CuentaCatalogo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CuentaCatalogo/Create
        [HttpPost]
        public ActionResult Create(CuentaCatalogo cuentaCatalogo)
        {
            try
            {
                //creo la instancia de contexto de datos y guardo parametros en BD
                using (var conexion = new Models.contextoDatos())
                {
                    if (ModelState.IsValid)
                    {
                        conexion.CuentaCatalogo.Add(cuentaCatalogo);
                        conexion.SaveChanges();
                        return RedirectToAction("Index", "CuentaCatalogo");
                    }
                    return View(cuentaCatalogo);
                }
            }
            catch
            {
                return View(cuentaCatalogo);

            }
        }
    }
}
