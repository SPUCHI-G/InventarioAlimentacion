using Inventario_Alimentacion.Filters;
using Inventario_Alimentacion.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Inventario_Alimentacion.Controllers
{
    public class ConsultaInventarioController : Controller
    {
        // GET: ConsultaInventario
        private BD_InventarioEntities db = new BD_InventarioEntities();
        [AuthorizeUser(idOperacion: 2)]
        public ActionResult Inventario()
        {
            string c = "0";
            string s = "0";
            string n = "0";
            ViewBag.Lista = db.Database.SqlQuery<spConsultar_Inventario_Result>("spConsultar_Inventario @categoria,@subcategoria,@producto",
                                                               new SqlParameter("@categoria", c),
                                                               new SqlParameter("@subcategoria", s),
                                                               new SqlParameter("@producto", n));
            return View();
        }
        [HttpPost]
        public ActionResult Inventario(String ID_Categoria, String ID_Subcategoria, String ID_NomProducto) // falta el filtro por fecha y el filtrado para que busque dentro deuna frase
        {

            if (ID_Categoria.Equals("") && ID_Subcategoria.Equals("") && ID_NomProducto.Equals(""))
            {
                ID_Categoria = "0";
                ID_Subcategoria = "0";
                ID_NomProducto = "0";
                ViewBag.Lista = db.Database.SqlQuery<spConsultar_Inventario_Result>("spConsultar_Inventario @categoria,@subcategoria,@producto",
                                 new SqlParameter("@categoria", ID_Categoria),
                                 new SqlParameter("@subcategoria", ID_Subcategoria),
                                  new SqlParameter("@producto", ID_NomProducto));
            }
            else
            {
                if (!ID_Categoria.Equals(""))
                {
                    ID_Subcategoria = "0";
                    ID_NomProducto = "0";
                    ViewBag.Lista = db.Database.SqlQuery<spConsultar_Inventario_Result>("spConsultar_Inventario @categoria,@subcategoria,@producto",
                                                                   new SqlParameter("@categoria", ID_Categoria),
                                                                   new SqlParameter("@subcategoria", ID_Subcategoria),
                                                                   new SqlParameter("@producto", ID_NomProducto));
                }
                else
                {
                    if (!ID_Subcategoria.Equals(""))
                    {
                        ID_Categoria = "0";
                        ID_NomProducto = "0";
                        ViewBag.Lista = db.Database.SqlQuery<spConsultar_Inventario_Result>("spConsultar_Inventario @categoria,@subcategoria,@producto",
                                                                       new SqlParameter("@categoria", ID_Categoria),
                                                                       new SqlParameter("@subcategoria", ID_Subcategoria),
                                                                       new SqlParameter("@producto", ID_NomProducto));
                    }
                    else
                    {
                        if (!ID_NomProducto.Equals(""))
                        {
                            ID_Categoria = "0";
                            ID_Subcategoria = "0";
                            ViewBag.Lista = db.Database.SqlQuery<spConsultar_Inventario_Result>("spConsultar_Inventario @categoria,@subcategoria,@producto",
                                                                           new SqlParameter("@categoria", ID_Categoria),
                                                                           new SqlParameter("@subcategoria", ID_Subcategoria),
                                                                           new SqlParameter("@producto", ID_NomProducto));
                        }

                    }

                }

            }


            return View();
        }


        public ActionResult Informes()
        {
            return View();
        }
        public ActionResult GenerarExcel()
        {

            ViewBag.Lista = db.Database.SqlQuery<spGuardarExcel_Result>("spGuardarExcel");


            StringBuilder builder = new StringBuilder();

            //Agregamos las cabezeras 
            builder
            .Append("CATEGORIAS").Append(";")
            .Append("SUBCATEGORIAS").Append(";")
            .Append("PRODUCTOS").Append(";")
            .Append("PROVEEDOR").Append(";")
            .Append("SALDO").Append(";")
            .Append("PUNTO PEDIDO").Append(";")
            .Append("CANTIDAD SOLICITAR").Append(";")
            .Append("UNIDAD MEDIDA").Append(";")
            .Append("VALOR UNITARIO").Append(";")
            .Append("VALOR TOTAL").Append(";");
            builder.Append("\n");

            foreach (spGuardarExcel_Result item in ViewBag.Lista)
            {

                builder
                .Append(item.Cat_Nombre).Append(";")
                .Append(item.Subca_Nombre).Append(";")
                .Append(item.NomProducto).Append(";")
                .Append("").Append(";")
                .Append(item.Saldo).Append(";")
                .Append(item.Min).Append(";")
                .Append(item.Punto_pedido).Append(";")
                .Append("").Append(";")
                .Append(item.Ent_Precio_Unitario).Append(";")
                .Append(item.Ent_Precio_Total).Append(";");
                builder.Append("\n");// agregamos una nueva fila 
            }


            // Lo encodeamos con UTF8 para mostrar los acentos correctamente.
            var excelBytes = Encoding.UTF8.GetBytes(builder.ToString());
            var excelConUT8Encoding = Encoding.UTF8.GetPreamble().Concat(excelBytes).ToArray();

            // guardamos el contenido del archivo en la ruta especificada
            var rutaExcel = Server.MapPath("~/App_Data/excel.xls");
            System.IO.File.WriteAllBytes(rutaExcel, excelConUT8Encoding);
            return File(rutaExcel, "text/xls", "Informe Indicadores de punto de partida.xls");

        }
        public ActionResult GenerarExcel_Vencidos()
        {

            ViewBag.Lista = db.Database.SqlQuery<spGuardarExcel_Vencidos_Result>("spGuardarExcel_Vencidos");


            StringBuilder builder = new StringBuilder();

            //Agregamos las cabezeras 
            builder
            .Append("CATEGORIAS").Append(";")
            .Append("SUBCATEGORIAS").Append(";")
            .Append("PRODUCTOS").Append(";")
            .Append("CANTIDAD PESO").Append(";")
            .Append("VALOR UNITARIO").Append(";")
            .Append("VALOR TOTAL VENCIDO").Append(";");
            builder.Append("\n");

            foreach (spGuardarExcel_Vencidos_Result item in ViewBag.Lista)
            {

                builder
                .Append(item.Cat_Nombre).Append(";")
                .Append(item.Subca_Nombre).Append(";")
                .Append(item.NomProducto).Append(";")
                .Append(item.Ent_Cantidad_Peso).Append(";")
                .Append(item.Ent_Precio_Unitario).Append(";")
                .Append(item.Ent_Precio_Total).Append(";");
                builder.Append("\n");// agregamos una nueva fila 
            }


            // Lo encodeamos con UTF8 para mostrar los acentos correctamente.
            var excelBytes = Encoding.UTF8.GetBytes(builder.ToString());
            var excelConUT8Encoding = Encoding.UTF8.GetPreamble().Concat(excelBytes).ToArray();

            // guardamos el contenido del archivo en la ruta especificada
            var rutaExcel = Server.MapPath("~/App_Data/excel.xls");
            System.IO.File.WriteAllBytes(rutaExcel, excelConUT8Encoding);
            return File(rutaExcel, "text/xls", "Informe productos vencidos.xls");

        }
        public ActionResult GenerarExcel_Inventario()
        {

            ViewBag.Lista = db.Database.SqlQuery<spGuardarExcel_Inventario_Result>("spGuardarExcel_Inventario");


            StringBuilder builder = new StringBuilder();

            //Agregamos las cabezeras 
            builder
            .Append("CATEGORIAS").Append(";")
            .Append("SUBCATEGORIAS").Append(";")
            .Append("PRODUCTOS").Append(";")
            .Append("CANTIDAD INGRESO").Append(";")
            .Append("CANTIDAD SALIDA").Append(";")
            .Append("SALDO").Append(";")
            .Append("VALOR TOTAL").Append(";");
            builder.Append("\n");

            foreach (spGuardarExcel_Inventario_Result item in ViewBag.Lista)
            {

                builder
                .Append(item.Cat_Nombre).Append(";")
                .Append(item.Subca_Nombre).Append(";")
                .Append(item.NomProducto).Append(";")
                .Append(item.can_ent).Append(";")
                .Append(item.can_sali).Append(";")
                .Append(item.Saldo).Append(";")
                .Append(item.valor_total).Append(";");
                builder.Append("\n");// agregamos una nueva fila 
            }


            // Lo encodeamos con UTF8 para mostrar los acentos correctamente.
            var excelBytes = Encoding.UTF8.GetBytes(builder.ToString());
            var excelConUT8Encoding = Encoding.UTF8.GetPreamble().Concat(excelBytes).ToArray();

            // guardamos el contenido del archivo en la ruta especificada
            var rutaExcel = Server.MapPath("~/App_Data/excel.xls");
            System.IO.File.WriteAllBytes(rutaExcel, excelConUT8Encoding);
            return File(rutaExcel, "text/xls", "Inventario mensual.xls");

        }
        public ActionResult GenerarExcel_Det_Ent()
        {
            ViewBag.Lista2 = db.Database.SqlQuery<spGuardarExcel_Det_Ent_Result>("spGuardarExcel_Det_Ent");


            StringBuilder builder = new StringBuilder();

            //Agregamos las cabezeras 
            builder
            .Append("FECHA INGRESO").Append(";")
            .Append("EMPLEADO").Append(";")
            .Append("CATEGORÍA").Append(";")
            .Append("SUBCATEGORIAS").Append(";")
            .Append("PRODUCTO").Append(";")
            .Append("CANTIDAD PESO").Append(";")
            .Append("TIPO UNIDAD").Append(";")
            .Append("TEMPERATURA").Append(";")
            .Append("FECHA-VENCIMIENTO").Append(";")
            .Append("TIPO DE ENTRADA").Append(";")
            .Append("PROVEEDOR").Append(";")
            .Append("DONANTE").Append(";")
            .Append("FACTURA").Append(";")
            .Append("PRECIO UNIDAD").Append(";")
            .Append("IVA%").Append(";")
            .Append("DESCUENTO%").Append(";")
            .Append("PRECIO TOTAL").Append(";");
            builder.Append("\n");

            foreach (spGuardarExcel_Det_Ent_Result item in ViewBag.Lista2)
            {

                builder
                .Append(item.Ent_Fecha).Append(";")
                .Append(item.Emp_Nombre).Append(";")
                .Append(item.Cat_Nombre).Append(";")
                .Append(item.Subca_Nombre).Append(";")
                .Append(item.NomProducto).Append(";")
                .Append(item.Ent_Cantidad_Peso).Append(";")
                .Append(item.Nombre_Unidad).Append(";")
                .Append(item.Ent_Temperatura).Append(";")
                .Append(item.Ent_FechVencim).Append(";")
                .Append(item.Tipo_Nombre).Append(";")
                .Append(item.Prov_Nombre).Append(";")
                .Append(item.Don_Nombre).Append(";")
                .Append(item.Ent_Factura).Append(";")
                .Append(item.Ent_Precio_Unitario).Append(";")
                .Append(item.Ent_iva).Append(";")
                .Append(item.Ent_Descuento).Append(";")
                .Append(item.Ent_Precio_Total).Append(";");
                builder.Append("\n");// agregamos una nueva fila 
            }


            // Lo encodeamos con UTF8 para mostrar los acentos correctamente.
            var excelBytes = Encoding.UTF8.GetBytes(builder.ToString());
            var excelConUT8Encoding = Encoding.UTF8.GetPreamble().Concat(excelBytes).ToArray();

            // guardamos el contenido del archivo en la ruta especificada
            var rutaExcel = Server.MapPath("~/App_Data/excel.xls");
            System.IO.File.WriteAllBytes(rutaExcel, excelConUT8Encoding);
            return File(rutaExcel, "text/xls", "Entradas.xls");

        }
        public ActionResult GenerarExcel_Det_Sal()
        {
            ViewBag.Lista3 = db.Database.SqlQuery<spGuardarExcel_Det_Sal_Result>("spGuardarExcel_Det_Sal");


            StringBuilder builder = new StringBuilder();

            //Agregamos las cabezeras 
            builder
            .Append("FECHA SALIDA").Append(";")
            .Append("EMPLEADO").Append(";")
            .Append("CATEGORIAS").Append(";")
            .Append("SUBCATEGORIAS").Append(";")
            .Append("PRODUCTO").Append(";")
            .Append("CANTIDAD PESO").Append(";")
            .Append("TIPO UNIDAD").Append(";")
            .Append("TIPO DE SALIDA").Append(";")
            .Append("OBSERVACIONES").Append(";");
            builder.Append("\n");

            foreach (spGuardarExcel_Det_Sal_Result item in ViewBag.Lista3)
            {

                builder
                .Append(item.Sali_Fecha).Append(";")
                .Append(item.Emp_Nombre).Append(";")
                .Append(item.Cat_Nombre).Append(";")
                .Append(item.Subca_Nombre).Append(";")
                .Append(item.NomProducto).Append(";")
                .Append(item.Sal_Cantidad_Peso).Append(";")
                .Append(item.Nombre_Unidad).Append(";")
                .Append(item.Tipo_Nombre).Append(";")
                .Append(item.Sal_Observacion).Append(";");
                builder.Append("\n");// agregamos una nueva fila 
            }


            // Lo encodeamos con UTF8 para mostrar los acentos correctamente.
            var excelBytes = Encoding.UTF8.GetBytes(builder.ToString());
            var excelConUT8Encoding = Encoding.UTF8.GetPreamble().Concat(excelBytes).ToArray();

            // guardamos el contenido del archivo en la ruta especificada
            var rutaExcel = Server.MapPath("~/App_Data/excel.xls");
            System.IO.File.WriteAllBytes(rutaExcel, excelConUT8Encoding);
            return File(rutaExcel, "text/xls", "Salidas.xls");

        }
        public ActionResult Vencidos() // falta el filtro por fecha y el filtrado para que busque dentro deuna frase
        {
            String c = "0";
            String s = "0";
            String n = "0";

            ViewBag.Lista = db.Database.SqlQuery<spBuscar_Vencidos_Result>("spBuscar_Vencidos @categoria,@subcategoria,@producto",
                                                               new SqlParameter("@categoria", c),
                                                               new SqlParameter("@subcategoria", s),
                                                               new SqlParameter("@producto", n));

            return View();
        }

        [HttpPost]
        public ActionResult Vencidos(String ID_Categoria, String ID_Subcategoria, String ID_NomProducto) // falta el filtro por fecha y el filtrado para que busque dentro deuna frase
        {
            if (ID_Categoria.Equals("") && ID_Subcategoria.Equals("") && ID_NomProducto.Equals(""))
            {
                ID_Categoria = "0";
                ID_Subcategoria = "0";
                ID_NomProducto = "0";
                ViewBag.Lista = db.Database.SqlQuery<spBuscar_Vencidos_Result>("spBuscar_Vencidos @categoria,@subcategoria,@producto",
                                                               new SqlParameter("@categoria", ID_Categoria),
                                                               new SqlParameter("@subcategoria", ID_Subcategoria),
                                                               new SqlParameter("@producto", ID_NomProducto));
            }
            else
            {
                if (!ID_Categoria.Equals(""))
                {
                    ID_Subcategoria = "0";
                    ID_NomProducto = "0";
                    ViewBag.Lista = db.Database.SqlQuery<spBuscar_Vencidos_Result>("spBuscar_Vencidos @categoria,@subcategoria,@producto",
                                                                   new SqlParameter("@categoria", ID_Categoria),
                                                                   new SqlParameter("@subcategoria", ID_Subcategoria),
                                                                   new SqlParameter("@producto", ID_NomProducto));
                }
                else
                {
                    if (!ID_Subcategoria.Equals(""))
                    {
                        ID_Categoria = "0";
                        ID_NomProducto = "0";
                        ViewBag.Lista = db.Database.SqlQuery<spBuscar_Vencidos_Result>("spBuscar_Vencidos @categoria,@subcategoria,@producto",
                                                                       new SqlParameter("@categoria", ID_Categoria),
                                                                       new SqlParameter("@subcategoria", ID_Subcategoria),
                                                                       new SqlParameter("@producto", ID_NomProducto));
                    }
                    else
                    {
                        if (!ID_NomProducto.Equals(""))
                        {
                            ID_Categoria = "0";
                            ID_Subcategoria = "0";
                            ViewBag.Lista = db.Database.SqlQuery<spBuscar_Vencidos_Result>("spBuscar_Vencidos @categoria,@subcategoria,@producto",
                                                                           new SqlParameter("@categoria", ID_Categoria),
                                                                           new SqlParameter("@subcategoria", ID_Subcategoria),
                                                                           new SqlParameter("@producto", ID_NomProducto));
                        }

                    }

                }

            }


            return View();
        }
        public ActionResult Proximos() // falta el filtro por fecha y el filtrado para que busque dentro deuna frase
        {
            String c = "0";
            String s = "0";
            String n = "0";

            ViewBag.Listap = db.Database.SqlQuery<spBuscar_ProxVencer_Result>("spBuscar_ProxVencer @categoria,@subcategoria,@producto",
                                                           new SqlParameter("@categoria", c),
                                                           new SqlParameter("@subcategoria", s),
                                                           new SqlParameter("@producto", n));
            return View();
        }

        [HttpPost]
        public ActionResult Proximos(String ID_Categoria, String ID_Subcategoria, String ID_NomProducto) // falta el filtro por fecha y el filtrado para que busque dentro deuna frase
        {
            if (ID_Categoria.Equals("") && ID_Subcategoria.Equals("") && ID_NomProducto.Equals(""))
            {
                ID_Categoria = "0";
                ID_Subcategoria = "0";
                ID_NomProducto = "0";
                ViewBag.Lista = db.Database.SqlQuery<spBuscar_Vencidos_Result>("spBuscar_Vencidos @categoria,@subcategoria,@producto",
                                                               new SqlParameter("@categoria", ID_Categoria),
                                                               new SqlParameter("@subcategoria", ID_Subcategoria),
                                                               new SqlParameter("@producto", ID_NomProducto));
            }
            else
            {
                if (!ID_Categoria.Equals(""))
                {
                    ID_Subcategoria = "0";
                    ID_NomProducto = "0";
                    ViewBag.Lista = db.Database.SqlQuery<spBuscar_Vencidos_Result>("spBuscar_Vencidos @categoria,@subcategoria,@producto",
                                                                   new SqlParameter("@categoria", ID_Categoria),
                                                                   new SqlParameter("@subcategoria", ID_Subcategoria),
                                                                   new SqlParameter("@producto", ID_NomProducto));
                }
                else
                {
                    if (!ID_Subcategoria.Equals(""))
                    {
                        ID_Categoria = "0";
                        ID_NomProducto = "0";
                        ViewBag.Lista = db.Database.SqlQuery<spBuscar_Vencidos_Result>("spBuscar_Vencidos @categoria,@subcategoria,@producto",
                                                                       new SqlParameter("@categoria", ID_Categoria),
                                                                       new SqlParameter("@subcategoria", ID_Subcategoria),
                                                                       new SqlParameter("@producto", ID_NomProducto));
                    }
                    else
                    {
                        if (!ID_NomProducto.Equals(""))
                        {
                            ID_Categoria = "0";
                            ID_Subcategoria = "0";
                            ViewBag.Lista = db.Database.SqlQuery<spBuscar_Vencidos_Result>("spBuscar_Vencidos @categoria,@subcategoria,@producto",
                                                                           new SqlParameter("@categoria", ID_Categoria),
                                                                           new SqlParameter("@subcategoria", ID_Subcategoria),
                                                                           new SqlParameter("@producto", ID_NomProducto));
                        }

                    }

                }
            }
            return View();
        }
    }
}