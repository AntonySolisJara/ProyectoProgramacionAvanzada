using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//Agregar libreria de referencia
using Indicadores.Models;
using System.Net;
using System.Data.Entity;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.OleDb;
using System.IO;
using System.Data.Entity.Infrastructure;

namespace Indicadores.Controllers
{
    public class MantIndicadoresController : Controller
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

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)

        {
            try
            {
                string filename = Guid.NewGuid() + Path.GetExtension(file.FileName);
                string filepath = "/excelfolder/" + filename;
                file.SaveAs(Path.Combine(Server.MapPath("/excelfolder"), filename));
                InsertExceldata(filepath, filename);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: MantIndicadores/Details/5
        public ActionResult Details(int? id)
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
                    return RedirectToAction("Index");
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
               new string[] { "CtaCatalogo","SiglasEst","SiglasPer","SiglasUni" }))
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

        #region METODO SUBIR EXCEL
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        OleDbConnection Econ;
        private void ExcelConn(string filepath)

        {
            string constr = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0 Xml;HDR=YES;""", filepath);
            Econ = new OleDbConnection(constr);
        }

        private void InsertExceldata(string fileepath, string filename)

        {

            string fullpath = Server.MapPath("/excelfolder/") + filename;

            ExcelConn(fullpath);

            string query = string.Format("Select * from [{0}]", "Indicador$");

            OleDbCommand Ecom = new OleDbCommand(query, Econ);

            Econ.Open();



            DataSet ds = new DataSet();

            OleDbDataAdapter oda = new OleDbDataAdapter(query, Econ);

            Econ.Close();

            oda.Fill(ds);



            DataTable dt = ds.Tables[0];



            SqlBulkCopy objbulk = new SqlBulkCopy(con);

            objbulk.DestinationTableName = "Indicador";
            objbulk.ColumnMappings.Add("CtaCatalogo", "CtaCatalogo");
            objbulk.ColumnMappings.Add("SiglasEst", "SiglasEst");
            objbulk.ColumnMappings.Add("SiglasPer", "SiglasPer");
            objbulk.ColumnMappings.Add("SiglasUni", "SiglasUni");

            con.Open();

            objbulk.WriteToServer(dt);

            con.Close();

        }
        #endregion
    }
}
