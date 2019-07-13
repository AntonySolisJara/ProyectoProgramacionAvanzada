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

namespace Indicadores.Controllers
{
    public class MantMetadataController : Controller
    {
        #region METODOS CRUD
        //Creamos instancia del Modelo Metadata para llamar los metodos Listar
        private Metadata metadata = new Metadata();

        // GET: MantMetadata
        public ActionResult Index()
        {
            return View(metadata.ListarMetadata());
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

        // GET: MantMetadata/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                using (var conexion = new Models.contextoDatos())
                {
                    Metadata met = conexion.Metadata.Find(id);
                    if (met == null)
                    {
                        return HttpNotFound();
                    }
                    else
                    {
                        return View(met);
                    }
                }
            }
        }

        // GET: MantMetadata/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MantMetadata/Create
        [HttpPost]
        public ActionResult Create(Metadata metadata)
        {
            try
            {
                //creo la instancia de contexto de datos y guardo parametros en BD
                using (var conexion = new Models.contextoDatos())
                {
                    if (ModelState.IsValid)
                    {
                        conexion.Metadata.Add(metadata);
                        conexion.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    return View(metadata);
                }
            }
            catch
            {
                return View(metadata);

            }
        }

        // GET: MantMetadata/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                using (var conexion = new Models.contextoDatos())
                {
                    Metadata met = conexion.Metadata.Find(id);
                    if (met == null)
                    {
                        return HttpNotFound();
                    }
                    else
                    {
                        return View(met);
                    }
                }
            }
        }

        // POST: MantMetadata/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Metadata metadata)
        {
            try
            {

                //Creamos la instancia de contexto de datos
                Models.contextoDatos conexion = new Models.contextoDatos();

                //Se carga una lista con los registros de la base de datos
                List<Models.Metadata> lista = (from per in conexion.Metadata
                                                   select per).ToList<Models.Metadata>();

                //Se recorre cada registro de la lista y se modifica el que coincida con la condicion de la busqueda
                foreach (Models.Metadata p in lista)
                {
                    if (p.SiglasMet == id)
                    {
                        p.SiglasMet = metadata.SiglasMet;
                        p.NomMetadata = metadata.NomMetadata;
                        p.DescMetadata = metadata.DescMetadata;
                        conexion.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                return View(metadata);
            }
            catch
            {
                return View(metadata);
            }
        }

        // GET: MantMetadata/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                using (var conexion = new Models.contextoDatos())
                {
                    Metadata met = conexion.Metadata.Find(id);
                    if (met == null)
                    {
                        return HttpNotFound();
                    }
                    else
                    {
                        return View(met);
                    }
                }
            }
        }

        // POST: MantMetadata/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, Metadata metadata)
        {
            try
            {

                //Creamos la instancia de contexto de datos
                Models.contextoDatos conexion = new Models.contextoDatos();

                //Se carga una lista con los registros de la base de datos
                List<Models.Metadata> lista = (from per in conexion.Metadata
                                               select per).ToList<Models.Metadata>();

                //Se recorre cada registro de la lista y se elimina el que coincida con la condicion de la busqueda
                foreach (Models.Metadata p in lista)
                {
                    if (p.SiglasMet == id)
                    {
                        conexion.Metadata.Remove(p);
                        conexion.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                return View(metadata);
            }
            catch
            {
                return View(metadata);
            }
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

            string query = string.Format("Select * from [{0}]", "Metadata$");

            OleDbCommand Ecom = new OleDbCommand(query, Econ);

            Econ.Open();



            DataSet ds = new DataSet();

            OleDbDataAdapter oda = new OleDbDataAdapter(query, Econ);

            Econ.Close();

            oda.Fill(ds);



            DataTable dt = ds.Tables[0];



            SqlBulkCopy objbulk = new SqlBulkCopy(con);

            objbulk.DestinationTableName = "Metadata";
            objbulk.ColumnMappings.Add("SiglasMet", "SiglasMet");
            objbulk.ColumnMappings.Add("NomMetadata", "NomMetadata");
            objbulk.ColumnMappings.Add("DescMetadata", "DescMetadata");

            con.Open();

            objbulk.WriteToServer(dt);

            con.Close();

        }
        #endregion
    }
}
