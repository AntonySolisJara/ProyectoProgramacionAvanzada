using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Indicadores.Models;

namespace Indicadores.Controllers
{
    public class IndicadorPosvalorController : Controller
    {
        private IndicadorPosvalor indPosvalor = new IndicadorPosvalor();
        contextoDatos contexto = new contextoDatos();

        // GET: IndicadorPosvalor
        public ActionResult Index()
        {
            return View(indPosvalor.ListarIndicadorPos());
        }

        // GET: IndicadorPosvalor/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: IndicadorPosvalor/Create/5
        public ActionResult Create()
        {
            ListaIndicadores();
            ListaPosvalores();
            return View();
        }

        // POST: IndicadorPosvalor/Create/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdIndicador,IdPosvalor,Orden")]IndicadorPosvalor indicadorPosvalor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    contexto.IndicadorPosvalor.Add(indicadorPosvalor);
                    contexto.SaveChanges();
                    return RedirectToAction("Create");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                ModelState.AddModelError("", "No se puede guardar. Intente nuevamente, si el problema persiste, contacte al administrador del sistema.");
            }
            ListaIndicadores(indicadorPosvalor.IdIndicador);
            ListaPosvalores(indicadorPosvalor.IdPosvalor);
            return View(indicadorPosvalor);
        }

        // GET: IndicadorPosvalor/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: IndicadorPosvalor/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: IndicadorPosvalor/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: IndicadorPosvalor/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private void ListaIndicadores(object seleccionarSiglas = null)
        {
            var consultaIndicadores = from d in contexto.Indicador
                                    orderby d.CtaCatalogo
                                    select d;
            ViewBag.IdIndicador = new SelectList(consultaIndicadores, "IdIndicador", "CtaCatalogo", seleccionarSiglas);
        }

        private void ListaPosvalores(object seleccionarSiglas = null)
        {
            var consultaPosvalores = from d in contexto.Posvalor
                                    orderby d.SiglasMet
                                    select d;
            ViewBag.IdPosvalor = new SelectList(consultaPosvalores, "IdPosvalor", "NomPosvalor", "SiglasMet", seleccionarSiglas);
        }
    }
}
