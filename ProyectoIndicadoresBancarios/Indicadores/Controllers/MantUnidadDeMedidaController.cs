using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//Agregar libreria de referencia
using Indicadores.Models;
using System.Net;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.OleDb;
using System.IO;

namespace Indicadores.Controllers
{
    public class MantUnidadDeMedidaController : Controller
    {

        #region METODOS CRUD
        //Creamos instancia del Modelo UnidadMedida para llamar los metodos Listar
        private UnidadMedida unidad = new UnidadMedida();

        // GET: MantUnidadDeMedida
        public ActionResult Index()
        {
            return View(unidad.ListarUnidad());
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

        // GET: MantUnidadDeMedida/Details/5
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
                    UnidadMedida uds = conexion.UnidadMedida.Find(id);
                    if (uds == null)
                    {
                        return HttpNotFound();
                    }
                    else
                    {
                        return View(uds);
                    }
                }
            }
        }

        // GET: MantUnidadDeMedida/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MantUnidadDeMedida/Create
        [HttpPost]
        public ActionResult Create(UnidadMedida unidad)
        {
            try
            {
                //creo la instancia de contexto de datos y guardo parametros en BD
                using (var conexion = new Models.contextoDatos())
                {
                    if (ModelState.IsValid)
                    {
                        conexion.UnidadMedida.Add(unidad);
                        conexion.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    return View(unidad);
                }
            }
            catch
            {
                return View(unidad);

            }
        }

        // GET: MantUnidadDeMedida/Edit/5
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
                    UnidadMedida uds = conexion.UnidadMedida.Find(id);
                    if (uds == null)
                    {
                        return HttpNotFound();
                    }
                    else
                    {
                        return View(uds);
                    }
                }
            }
        }

        // POST: MantUnidadDeMedida/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, UnidadMedida unidad)
        {
            try
            {

                //Creamos la instancia de contexto de datos
                Models.contextoDatos conexion = new Models.contextoDatos();

                //Se carga una lista con los registros de la base de datos
                List<Models.UnidadMedida> lista = (from per in conexion.UnidadMedida
                                                   select per).ToList<Models.UnidadMedida>();

                //Se recorre cada registro de la lista y se modifica el que coincida con la condicion de la busqueda
                foreach (Models.UnidadMedida p in lista)
                {
                    if (p.SiglasUni == id)
                    {
                        p.SiglasUni = unidad.SiglasUni;
                        p.DescUnidad = unidad.DescUnidad;
                        conexion.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                return View(unidad);
            }
            catch
            {
                return View(unidad);
            }
        }

        // GET: MantUnidadDeMedida/Delete/5
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
                    UnidadMedida uds = conexion.UnidadMedida.Find(id);
                    if (uds == null)
                    {
                        return HttpNotFound();
                    }
                    else
                    {
                        return View(uds);
                    }
                }
            }
        }
        
        // POST: MantUnidadDeMedida/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, UnidadMedida unidad)
        {
            try
            {
                //Creamos la instancia de contexto de datos
                Models.contextoDatos conexion = new Models.contextoDatos();

                //Se carga una lista con los registros de la base de datos
                List<Models.UnidadMedida> lista = (from per in conexion.UnidadMedida
                                                   select per).ToList<Models.UnidadMedida>();

                //Se recorre cada registro de la lista y se elimina el que coincida con la condicion de la busqueda
                foreach (Models.UnidadMedida p in lista)
                {
                    if (p.SiglasUni == id)
                    {
                        conexion.UnidadMedida.Remove(p);
                        conexion.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }

                return View(unidad);
            }
            catch
            {
                return View(unidad);
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

            string query = string.Format("Select * from [{0}]", "UnidadMedida$");

            OleDbCommand Ecom = new OleDbCommand(query, Econ);

            Econ.Open();



            DataSet ds = new DataSet();

            OleDbDataAdapter oda = new OleDbDataAdapter(query, Econ);

            Econ.Close();

            oda.Fill(ds);



            DataTable dt = ds.Tables[0];



            SqlBulkCopy objbulk = new SqlBulkCopy(con);

            objbulk.DestinationTableName = "UnidadMedida";

            objbulk.ColumnMappings.Add("SiglasUni", "SiglasUni");

            objbulk.ColumnMappings.Add("DescUnidad", "DescUnidad");

            con.Open();

            objbulk.WriteToServer(dt);

            con.Close();

        }
        #endregion
    }
}
