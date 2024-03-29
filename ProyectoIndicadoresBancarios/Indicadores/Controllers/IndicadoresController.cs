﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//Agregar libreria de referencia
using Indicadores.Models;
using System.Net;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Indicadores.Controllers
{
    public class IndicadoresController : Controller
    {
        #region METODOS CRUD
        private contextoDatos contexto = new contextoDatos();
        //Creamos instancia del Modelo Indicador para llamar los metodos Listar
        private Indicador indicador = new Indicador();

        // GET: MantIndicadores
        public ActionResult Index()
        {
            return View(indicador.ListarIndicador());
        }

        // GET: MantIndicadores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                using (var conexion = new Models.contextoDatos())
                {
                    Indicador ind = conexion.Indicador.Find(id);
                    if (ind == null)
                    {
                        return HttpNotFound();
                    }
                    else
                    {
                        return View(ind);
                    }
                }
            }
        }

        // POST: MantIndicadores/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Indicador indicador)
        {
            try
            {

                //Creamos la instancia de contexto de datos
                Models.contextoDatos conexion = new Models.contextoDatos();

                //Se carga una lista con los registros de la base de datos
                List<Models.Indicador> lista = (from per in conexion.Indicador
                                                select per).ToList<Models.Indicador>();

                //Se recorre cada registro de la lista y se elimina el que coincida con la condicion de la busqueda
                foreach (Models.Indicador p in lista)
                {
                    if (p.IdIndicador == id)
                    {
                        conexion.Indicador.Remove(p);
                        conexion.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                return View(indicador);
            }
            catch
            {
                return View(indicador);
            }
        }

        // GET: MantPosvalores/Create/5
        public ActionResult Create()
        {
            ListaCtaCatalogo();
            ListaEstado();
            ListaPeriodo();
            ListaUnidad();
            return View();
        }

        // POST: MantPosvalores/Create/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CtaCatalogo,SiglasEst,SiglasPer,SiglasUni")]Indicador indicador)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    contexto.Indicador.Add(indicador);
                    contexto.SaveChanges();
                    return RedirectToAction("Index", "Indicadores");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                ModelState.AddModelError("", "No se puede guardar. Intente nuevamente, si el problema persiste, contacte al administrador del sistema.");
            }
            ListaCtaCatalogo(indicador.CtaCatalogo);
            ListaEstado(indicador.SiglasEst);
            ListaPeriodo(indicador.SiglasPer);
            ListaUnidad(indicador.SiglasUni);
            return View(indicador);
        }

        // GET: MantPosvalores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Indicador indicador = contexto.Indicador.Find(id);
            if (indicador == null)
            {
                return HttpNotFound();
            }
            ListaCtaCatalogo(indicador.CtaCatalogo);
            ListaEstado(indicador.SiglasEst);
            ListaPeriodo(indicador.SiglasPer);
            ListaUnidad(indicador.SiglasUni);
            return View(indicador);
        }

        // POST: MantPosvalores/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var editarPosvalor = contexto.Indicador.Find(id);
            if (TryUpdateModel(editarPosvalor, "",
               new string[] { "CtaCatalogo", "SiglasEst", "SiglasPer", "SiglasUni" }))
            {
                try
                {
                    contexto.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    ModelState.AddModelError("", "No se puede guardar. Intente nuevamente, si el problema persiste, contacte al administrador del sistema.");
                }
            }
            ListaCtaCatalogo(indicador.CtaCatalogo);
            ListaEstado(indicador.SiglasEst);
            ListaPeriodo(indicador.SiglasPer);
            ListaUnidad(indicador.SiglasUni);
            return View(editarPosvalor);
        }

        private void ListaCtaCatalogo(object seleccionarSiglas = null)
        {
            var consultaCtaCatalogo = from d in contexto.CuentaCatalogo
                                      orderby d.CtaCatalogo
                                      select d;
            ViewBag.CtaCatalogo = new SelectList(consultaCtaCatalogo, "CtaCatalogo", "CtaCatalogo", seleccionarSiglas);
        }

        private void ListaEstado(object seleccionarSiglas = null)
        {
            var consultaEstado = from d in contexto.Estado
                                 orderby d.DescEstado
                                 select d;
            ViewBag.SiglasEst = new SelectList(consultaEstado, "SiglasEst", "DescEstado", seleccionarSiglas);
        }

        private void ListaPeriodo(object seleccionarSiglas = null)
        {
            var consultaPeriodo = from d in contexto.Periodo
                                  orderby d.DescPeriodo
                                  select d;
            ViewBag.SiglasPer = new SelectList(consultaPeriodo, "SiglasPer", "DescPeriodo", seleccionarSiglas);
        }

        private void ListaUnidad(object seleccionarSiglas = null)
        {
            var consultaMetadatas = from d in contexto.UnidadMedida
                                    orderby d.DescUnidad
                                    select d;
            ViewBag.SiglasUni = new SelectList(consultaMetadatas, "SiglasUni", "DescUnidad", seleccionarSiglas);
        }

        #endregion
    }
}
