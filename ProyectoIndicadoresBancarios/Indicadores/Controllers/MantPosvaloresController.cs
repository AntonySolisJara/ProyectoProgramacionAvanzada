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
    public class MantPosvaloresController : Controller
    {
        #region METODOS CRUD
        private contextoDatos contexto = new contextoDatos();
        //Creamos instancia del Modelo Posvalor para llamar los metodos Listar
        private Posvalor posvalor = new Posvalor();
        private Metadata metadata = new Metadata();

        // GET: MantPosvalores
        public ActionResult Index()
        {
            MenuViewModel menuViewModel = new MenuViewModel();
            menuViewModel.MenuMeta = contexto.Metadata.Where(menu => menu.SiglasMet ==
                null).ToList().Select(menu => new SelectListItem
                {
                    Value = menuViewModel.SiglasMet.ToString(),
                    Text = menuViewModel.SiglasMet
                }).ToList();
            return View(posvalor.ListarPosvalor());
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

        // GET: MantPosvalores/Details/5
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
                    Posvalor pos = conexion.Posvalor.Find(id);
                    if (pos == null)
                    {
                        return HttpNotFound();
                    }
                    else
                    {
                        return View(pos);
                    }
                }
            }
        }

        // GET: MantPosvalores/Delete/5
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
                    Posvalor pos = conexion.Posvalor.Find(id);
                    if (pos == null)
                    {
                        return HttpNotFound();
                    }
                    else
                    {
                        return View(pos);
                    }
                }
            }
        }

        // POST: MantPosvalores/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Posvalor posvalor)
        {
            try
            {
                //Creamos la instancia de contexto de datos
                Models.contextoDatos conexion = new Models.contextoDatos();

                //Se carga una lista con los registros de la base de datos
                List<Models.Posvalor> lista = (from per in conexion.Posvalor
                                                   select per).ToList<Models.Posvalor>();

                foreach (Models.Posvalor p in lista)
                {
                    if (p.IdPosvalor == id)
                    {
                        conexion.Posvalor.Remove(p);
                        conexion.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }

                return View(posvalor);
            }
            catch
            {
                return View(posvalor);
            }
        }

        // GET: MantPosvalores/Create/5
        public ActionResult Create()
        {
            ListaMetadatas();
            return View();
        }

        // POST: MantPosvalores/Create/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SiglasMet,SiglasPos,NomPosvalor,DescPosvalor")]Posvalor posvalor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    contexto.Posvalor.Add(posvalor);
                    contexto.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                ModelState.AddModelError("", "No se puede guardar. Intente nuevamente, si el problema persiste, contacte al administrador del sistema.");
            }
            ListaMetadatas(posvalor.SiglasMet);
            return View(posvalor);
        }

        // GET: MantPosvalores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Posvalor posvalor = contexto.Posvalor.Find(id);
            if (posvalor == null)
            {
                return HttpNotFound();
            }
            ListaMetadatas(posvalor.SiglasMet);
            return View(posvalor);
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
            var editarPosvalor = contexto.Posvalor.Find(id);
            if (TryUpdateModel(editarPosvalor, "",
               new string[] { "SiglasMet","SiglasPos","NomPosvalor","DescPosvalor" }))
            {
                try
                {
                    contexto.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            ListaMetadatas(editarPosvalor.SiglasMet);
            return View(editarPosvalor);
        }

        private void ListaMetadatas(object seleccionarSiglas = null)
        {
            var consultaMetadatas = from d in contexto.Metadata
                                   orderby d.NomMetadata
                                   select d;
            ViewBag.SiglasMet = new SelectList(consultaMetadatas, "SiglasMet", "NomMetadata", seleccionarSiglas);

           
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

            string query = string.Format("Select * from [{0}]", "Posvalor$");

            OleDbCommand Ecom = new OleDbCommand(query, Econ);

            Econ.Open();



            DataSet ds = new DataSet();

            OleDbDataAdapter oda = new OleDbDataAdapter(query, Econ);

            Econ.Close();

            oda.Fill(ds);



            DataTable dt = ds.Tables[0];



            SqlBulkCopy objbulk = new SqlBulkCopy(con);

            objbulk.DestinationTableName = "Posvalor";
            objbulk.ColumnMappings.Add("SiglasMet", "SiglasMet");
            objbulk.ColumnMappings.Add("SiglasPos", "SiglasPos");
            objbulk.ColumnMappings.Add("NomPosvalor", "NomPosvalor");
            objbulk.ColumnMappings.Add("DescPosvalor", "DescPosvalor");

            con.Open();

            objbulk.WriteToServer(dt);

            con.Close();

        }
        #endregion
    }
}
