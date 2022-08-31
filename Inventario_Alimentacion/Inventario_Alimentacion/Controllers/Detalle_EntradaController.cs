using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using Inventario_Alimentacion.Filters;
using Inventario_Alimentacion.Models;

namespace Inventario_Alimentacion.Controllers
{

    public class Detalle_EntradaController : Controller
    {
        private BD_InventarioEntities db = new BD_InventarioEntities();
        // GET: Detalle_Entrada

        [AuthorizeUser(idOperacion: 2)]
        public ActionResult Index(String FechaI, String FechaF)
        { 
            var obj = from s in db.Detalle_Entrada select s;
             
            
            if (!String.IsNullOrEmpty(FechaI)&&!String.IsNullOrEmpty(FechaF))
            {
                DateTime F = DateTime.Parse(FechaF);
                F = F.AddDays(1);
                ViewBag.Lista = db.Database.SqlQuery<spMostrarDeta_Entradas_Result>
                    ("spMostrarDeta_Entradas @inicio, @fin",
                                    new SqlParameter("@inicio", FechaI),
                                    new SqlParameter("@fin", F));
            }
            else
            {
                if(!String.IsNullOrEmpty(FechaI) && String.IsNullOrEmpty(FechaF))
                {
                    FechaF = DateTime.Now.ToString("yyyy/MM/dd"); // para evitar algun tipo de error al llenar, no se pueden enviar nullos
                    DateTime F = DateTime.Parse(FechaF);
                    F = F.AddDays(1);
                    ViewBag.Lista = db.Database.SqlQuery<spMostrarDeta_Entradas_Result>
                        ("spMostrarDeta_Entradas @inicio, @fin",
                                        new SqlParameter("@inicio", FechaI),
                                        new SqlParameter("@fin", F));
                }
                else
                {
                    if(String.IsNullOrEmpty(FechaI) && !String.IsNullOrEmpty(FechaF))
                    {
                        FechaI = DateTime.Today.Year+"-"+ DateTime.Today.Month + "-01"; // para evitar algun tipo de error al llenar, no se pueden enviar nullos
                        DateTime I = DateTime.Parse(FechaI);
                        I = I.AddMonths(-1);
                        DateTime F = DateTime.Parse(FechaF);
                        F = F.AddDays(1);
                        ViewBag.Lista = db.Database.SqlQuery<spMostrarDeta_Entradas_Result>
                            ("spMostrarDeta_Entradas @inicio, @fin",
                                            new SqlParameter("@inicio", I),
                                            new SqlParameter("@fin", F));
                    }
                    else
                    {
                        FechaI = DateTime.Today.Year + "-" + DateTime.Today.Month + "-01"; // para evitar algun tipo de error al llenar, no se pueden enviar nullos
                        FechaF = DateTime.Now.ToString("yyyy/MM/dd");
                        DateTime I = DateTime.Parse(FechaI);
                        I = I.AddMonths(-1);
                        DateTime F = DateTime.Parse(FechaF);
                        F = F.AddDays(1);
                        ViewBag.Lista = db.Database.SqlQuery<spMostrarDeta_Entradas_Result>
                            ("spMostrarDeta_Entradas @inicio, @fin",
                                            new SqlParameter("@inicio", I),
                                            new SqlParameter("@fin", F));
                    }
                }
                
            }
            return View();

        }



        // POST: Detalle_Entrada/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.

        [AuthorizeUser(idOperacion: 3)]
        public ActionResult Create()
        {
                ViewBag.TemporalList = db.Temporal.OrderByDescending(item => item.ID_Det_entr).ToList();
                ViewBag.fk_Donante = new SelectList(db.Donante, "ID_Donante", "Don_Nombre");
                ViewBag.fk_Proveedor = new SelectList(db.Proveedor, "Prov_Docum", "Prov_Nombre");
                ViewBag.fk_TipoEntrada = new SelectList(db.Tipo_Entrada, "ID_TipoEntrada", "Tipo_Nombre");
               //para llenar los drop down list de cascada
                ViewBag.CategoriaList = new SelectList(GetCategoriaList(), "ID_Categoria", "Cat_Nombre");
            
            
            return View();
        }

        // llena el dropdownlist  de categoria
        private List<Categoria> GetCategoriaList()
        {
            BD_InventarioEntities db = new BD_InventarioEntities();
            List<Categoria> Cat = db.Categoria.ToList();
            return Cat;
        }
        // llena el dropdownlist de subcategoria
        public ActionResult GetSubcategoriaList(int ID_Categoria)
        {
            BD_InventarioEntities db = new BD_InventarioEntities();
            List<Subcategoria> Sub = db.Subcategoria.Where(m => m.Categoria == ID_Categoria).ToList();
            ViewBag.SubcategoriaList = new SelectList(Sub, "ID_Subcategoria", "Subca_Nombre");
            return PartialView("DisplaySubcategoria");
        }
        // llena el dropdownlist de Nombres del producto
        public ActionResult GetNom_ProductoList(int ID_Subcategoria)
        {
            BD_InventarioEntities db = new BD_InventarioEntities();
            List<Nom_Producto> Nom = db.Nom_Producto.Where(m => m.Subcategoria == ID_Subcategoria).ToList();
            ViewBag.NomProductoList = new SelectList(Nom, "ID_NomProducto", "NomProducto");
            return PartialView("DisplayProducto");
        }
        public bool consultar_Prop_Adici(int id_Producto) // metodo que verifica si un producto necesita campos adicionales
        {
            var obj = from s in db.Nom_Producto select s;
            obj = obj.Where(J => J.ID_NomProducto == id_Producto);
            if (obj.Any()) //convierte el comando en bolleano
            {// si entra al primer if es porque existe este id
                var o = from s in db.Nom_Producto select s;
                o = o.Where(J => J.ID_NomProducto.Equals(id_Producto) && J.Prop_Adicion == true);
                if (o.Any()) 
                {// si entra al segundo if es porque necesita propiedades adicionales
                    return true; //desactiva el readonly
                }
                else
                {
                    return false;//activa el readonly
                }
            }
            else
            {
                return false;
            }
            
        }
        public ActionResult AgregarTemporal(int ID_Categoria, int ID_Subcategoria,int ID_NomProducto, decimal Ent_Cantidad_Peso, decimal Ent_Temperatura,
            string Ent_Factura, decimal Ent_Precio_Unitario, decimal Ent_Precio_Total, decimal Ent_iva, decimal Ent_Descuento, int fk_TipoEntrada,int fk_Proveedor, int fk_Donante,String Ent_FechVencim)
        {
            
                    if (!ID_Categoria.Equals(null) && !ID_Subcategoria.Equals(null) && !ID_NomProducto.Equals(null) && !Ent_Cantidad_Peso.Equals(null) && !Ent_Temperatura.Equals(null) && 
                    !Ent_Factura.Equals(null) && !Ent_Precio_Unitario.Equals(null) && !Ent_Precio_Total.Equals(null) && !Ent_iva.Equals(null) && !Ent_Descuento.Equals(null) && 
                    !fk_TipoEntrada.Equals(null) && !fk_Proveedor.Equals(null) && !fk_Donante.Equals(null))
                            {
                    
                                //trae el usuario que hizo el procedimiento 
                                //se crea la variable de salida
                                var returnCode = new SqlParameter();
                                returnCode.ParameterName = "@Id";
                                returnCode.SqlDbType = SqlDbType.Int;
                                returnCode.Direction = ParameterDirection.Output;
                                // se ejecuta el SP
                                db.Database.ExecuteSqlCommand("spCrearProducto @idc, @ids,@idp,@Id OUT", // primero se crea la combinacion en tabla producto si aun no existe
                                                     new SqlParameter("@idc", ID_Categoria),
                                                     new SqlParameter("@ids", ID_Subcategoria),
                                                     new SqlParameter("@idp", ID_NomProducto),
                                                     returnCode);
                                var Id = (int)returnCode.Value;//retorna el id del producto para guardarlo con los datos de detalle entrada en la tabla temporal
                                 DateTime F  ;
                                if (string.IsNullOrEmpty(Ent_FechVencim))
                                {
                                    F = DateTime.Parse("9999/12/30") ; // para evitar algun tipo de error al llenar, no se pueden enviar nullos desde EF
                                }else
                                {
                                     F= DateTime.Parse(Ent_FechVencim);
                                }
                                    var oUser = (Models.Usuario)Session["User"];// se consulta el usuario que esta logueado
                                    //crea una entrada especifica quien y aque horas se hacen movimientos de entrada
                                    db.Database.ExecuteSqlCommand("spLlenarEntrada @Usuario",
                                                     new SqlParameter("@Usuario", oUser.Usu_Nombre));

                // se hace el insert a travez de un SP para evitar errores con laPK de la  Tabla Detalle_Entrada
                db.Database.ExecuteSqlCommand("spCrearTemporal @empleado,@fk_TipoEntrada, @fk_Proveedor,@fk_Donante,@fk_Producto," +
                                    "@Ent_Precio_Total,@Ent_Factura,@Ent_Precio_Unitario,@Ent_iva,@Ent_Descuento,@Ent_Cantidad_Peso,@Ent_Temperatura,@Ent_FechVencim," +
                                    "@ID_Categoria,@ID_Subcategoria,@ID_NomProducto", // primero se crea la combinacion en tabla producto si aun no existe
                                                     new SqlParameter("@empleado", oUser.ID_Empleado),
                                                     new SqlParameter("@fk_TipoEntrada", fk_TipoEntrada),
                                                     new SqlParameter("@fk_Proveedor", fk_Proveedor),
                                                     new SqlParameter("@fk_Donante", fk_Donante),
                                                     new SqlParameter("@fk_Producto", Id),// el Id que retornamos anteriormente con el producto creado
                                                     new SqlParameter("@Ent_Precio_Total", Ent_Precio_Total),
                                                     new SqlParameter("@Ent_Factura", Ent_Factura),
                                                     new SqlParameter("@Ent_Precio_Unitario", Ent_Precio_Unitario),
                                                     new SqlParameter("@Ent_iva", Ent_iva),
                                                     new SqlParameter("@Ent_Descuento", Ent_Descuento),
                                                     new SqlParameter("@Ent_Cantidad_Peso", Ent_Cantidad_Peso),
                                                     new SqlParameter("@Ent_Temperatura", Ent_Temperatura),
                                                     new SqlParameter("@Ent_FechVencim", F), //envia la fecha modificada en caso de estar vacia ya que en EF no acepta null
                                                     new SqlParameter("@ID_Categoria", ID_Categoria),
                                                     new SqlParameter("@ID_Subcategoria", ID_Subcategoria),
                                                     new SqlParameter("@ID_NomProducto", ID_NomProducto));
                                return RedirectToAction("Create");
                }
            return View();
        }
        public ActionResult AgregarADetalle()
        {
            var obj = from s in db.Temporal select s;
            if (obj.Count() > 0) // si hay datos en la tabla temporal ejecute el procedimiento
            {
                db.Database.ExecuteSqlCommand("spAgregar_a_Detalle");
                //el return cierra la ventana emergente
                return Content(@"<body>
                       <script type='text/javascript'>
                         window.close();
                       </script>
                     </body> ");
            }
            return Content(@"<body>
                       <script type='text/javascript'>
                         window.close();
                       </script>
                     </body> ");

        }

        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Detalle_Entrada detalle_Entrada = db.Detalle_Entrada.Find(id);
            if (detalle_Entrada == null)
            {
                return HttpNotFound();
            }
            ViewBag.fk_Donante = new SelectList(db.Donante, "ID_Donante", "Don_Nombre", detalle_Entrada.fk_Donante);
            ViewBag.fk_Proveedor = new SelectList(db.Proveedor, "Prov_Docum", "Prov_Nombre", detalle_Entrada.fk_Proveedor);
            ViewBag.fk_TipoEntrada = new SelectList(db.Tipo_Entrada, "ID_TipoEntrada", "Tipo_Nombre", detalle_Entrada.fk_TipoEntrada);
            ViewBag.CategoriaList = new SelectList(GetCategoriaList(), "ID_Categoria", "Cat_Nombre");
            return View(detalle_Entrada);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(long ID_Det_entr, long fk_Entrada, int ID_Categoria, int ID_Subcategoria, int ID_NomProducto, decimal Ent_Cantidad_Peso, decimal Ent_Temperatura,
            string Ent_Factura, decimal Ent_Precio_Unitario, decimal Ent_Precio_Total, decimal Ent_iva, decimal Ent_Descuento, int fk_TipoEntrada, int fk_Proveedor, int fk_Donante, String Ent_FechVencim)
        {
            
            if (!ID_Categoria.Equals(null) && !ID_Subcategoria.Equals(null) && !ID_NomProducto.Equals(null) && !Ent_Cantidad_Peso.Equals(null) && !Ent_Temperatura.Equals(null) &&
                            !Ent_Factura.Equals(null) && !Ent_Precio_Unitario.Equals(null) && !Ent_Precio_Total.Equals(null) && !Ent_iva.Equals(null) && !Ent_Descuento.Equals(null) &&
                            !fk_TipoEntrada.Equals(null) && !fk_Proveedor.Equals(null) && !fk_Donante.Equals(null))
           {

                        var returnCode = new SqlParameter();
                        returnCode.ParameterName = "@Id";
                        returnCode.SqlDbType = SqlDbType.Int;
                        returnCode.Direction = ParameterDirection.Output;
                        // se ejecuta el SP
                        db.Database.ExecuteSqlCommand("spCrearProducto @idc, @ids,@idp,@Id OUT", // primero se crea la combinacion en tabla producto si aun no existe
                                             new SqlParameter("@idc", ID_Categoria),
                                             new SqlParameter("@ids", ID_Subcategoria),
                                             new SqlParameter("@idp", ID_NomProducto),
                                             returnCode);
                        var Id = (int)returnCode.Value;//retorna el id del producto para guardarlo con los datos de detalle entrada en la tabla temporal
                        DateTime F;
                        if (String.IsNullOrEmpty(Ent_FechVencim))
                        {
                         Ent_FechVencim = "9999/12/30"; // para evitar algun tipo de error al llenar, no se pueden enviar nullos desde EF
                            F = DateTime.Parse(Ent_FechVencim);
                        }
                        else
                        {
                            F = DateTime.Parse(Ent_FechVencim);
                        }


                        db.Database.ExecuteSqlCommand("spEditarDetEntrada @Id,@fk_Entrada,@fk_TipoEntrada, @fk_Proveedor,@fk_Donante,@fk_Producto," +
                                            "@Ent_Precio_Total,@Ent_Factura,@Ent_Precio_Unitario,@Ent_iva,@Ent_Descuento,@Ent_Cantidad_Peso,@Ent_Temperatura,@Ent_FechVencim", // primero se crea la combinacion en tabla producto si aun no existe
                                                             new SqlParameter("@Id", ID_Det_entr),
                                                             new SqlParameter("@fk_Entrada", fk_Entrada),
                                                             new SqlParameter("@fk_TipoEntrada", fk_TipoEntrada),
                                                             new SqlParameter("@fk_Proveedor", fk_Proveedor),
                                                             new SqlParameter("@fk_Donante", fk_Donante),
                                                             new SqlParameter("@fk_Producto", Id),// el Id que retornamos anteriormente con el producto creado
                                                             new SqlParameter("@Ent_Precio_Total", Ent_Precio_Total),
                                                             new SqlParameter("@Ent_Factura", Ent_Factura),
                                                             new SqlParameter("@Ent_Precio_Unitario", Ent_Precio_Unitario),
                                                             new SqlParameter("@Ent_iva", Ent_iva),
                                                             new SqlParameter("@Ent_Descuento", Ent_Descuento),
                                                             new SqlParameter("@Ent_Cantidad_Peso", Ent_Cantidad_Peso),
                                                             new SqlParameter("@Ent_Temperatura", Ent_Temperatura),
                                                             new SqlParameter("@Ent_FechVencim", F) //envia la fecha modificada en caso de estar vacia ya que en EF no acepta null
                                                             );
                        return RedirectToAction("Index", "Detalle_Entrada");
                    }
                    return RedirectToAction("Edit", "Detalle_Entrada");
            
        }
        public ActionResult Emergente()
        {
            var obj = from s in db.Temporal select s;
            if (obj.Count() > 0) // si hay datos en la tabla temporal ejecute 
            {
                ViewBag.TemporalList = db.Temporal.ToList();
                var cont = db.Temporal.ToList().LongCount();
            }
            else{
                return Content(@"<body>
                       <script type='text/javascript'>
                         window.close();
                       </script>
                     </body> ");
            }
            return View();
            
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
