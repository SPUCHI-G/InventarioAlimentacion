using Inventario_Alimentacion.Filters;
using Inventario_Alimentacion.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventario_Alimentacion.Controllers
{
    public class BuscarDetEmpController : Controller
    {
        // GET: BuscarDetEmp
        private BD_InventarioEntities db = new BD_InventarioEntities();
        [AuthorizeUser(idOperacion: 2)]
        public ActionResult RegisEmpleado(String id, String FechaI, String FechaF)
        {
            var Usu = from s in db.Detalle_Entrada select s;
            if (!String.IsNullOrEmpty(id))
            {
                if (String.IsNullOrEmpty(FechaI) && String.IsNullOrEmpty(FechaF))
                {
                    FechaI = DateTime.Today.Year + "-" + DateTime.Today.Month + "-01"; // para evitar algun tipo de error al llenar, no se pueden enviar nullos
                    FechaF = DateTime.Now.ToString("yyyy/MM/dd"); 
                    DateTime I = DateTime.Parse(FechaI);
                    I = I.AddMonths(-1);
                    DateTime F = DateTime.Parse(FechaF);
                    F = F.AddDays(1);
                    ViewBag.Lista = db.Database.SqlQuery<spBuscarDetEmpleado_up_Result>
                                    ("spBuscarDetEmpleado_up @idEmpleado,@inicio,@fin",
                                    new SqlParameter("@idEmpleado", id),
                                    new SqlParameter("@inicio", I),
                                    new SqlParameter("@fin", F));
                }
                else
                {
                    if (!String.IsNullOrEmpty(FechaI) && String.IsNullOrEmpty(FechaF))
                    {
                        FechaF = DateTime.Now.ToString("yyyy/MM/dd"); ;
                        DateTime I = DateTime.Parse(FechaI);
                        DateTime F = DateTime.Parse(FechaF);
                        F = F.AddDays(1);
                        ViewBag.Lista = db.Database.SqlQuery<spBuscarDetEmpleado_up_Result>
                                       ("spBuscarDetEmpleado_up @idEmpleado,@inicio,@fin",
                                       new SqlParameter("@idEmpleado", id),
                                       new SqlParameter("@inicio", I),
                                       new SqlParameter("@fin", F));
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(FechaI) && !String.IsNullOrEmpty(FechaF))
                        {
                            FechaI = DateTime.Today.Year + "-" + DateTime.Today.Month + "-01";
                            DateTime I = DateTime.Parse(FechaI);
                            I = I.AddMonths(-1);
                            DateTime F = DateTime.Parse(FechaF);
                            F = F.AddDays(1);
                            ViewBag.Lista = db.Database.SqlQuery<spBuscarDetEmpleado_up_Result>
                                            ("spBuscarDetEmpleado_up @idEmpleado,@inicio,@fin",
                                            new SqlParameter("@idEmpleado", id),
                                            new SqlParameter("@inicio", I),
                                            new SqlParameter("@fin", F));
                        }
                        else
                        {
                            if (!String.IsNullOrEmpty(FechaI) && !String.IsNullOrEmpty(FechaF))
                            {
                                DateTime I = DateTime.Parse(FechaI);
                                DateTime F = DateTime.Parse(FechaF);
                                F = F.AddDays(1);
                                ViewBag.Lista = db.Database.SqlQuery<spBuscarDetEmpleado_up_Result>
                                                ("spBuscarDetEmpleado_up @idEmpleado,@inicio,@fin",
                                                new SqlParameter("@idEmpleado", id),
                                                new SqlParameter("@inicio", I),
                                                new SqlParameter("@fin", F));
                            }

                            return View();
                        }
                    }
                }
            }
            else
                {
                    if (String.IsNullOrEmpty(id))
                    {
                        Int32 ID = 1234;// para evitar algun tipo de error al llenar, no se pueden enviar nullos
                        FechaI = DateTime.Today.Year + "-" + DateTime.Today.Month + "-01";
                        FechaF = DateTime.Now.ToString("yyyy/MM/dd");
                        DateTime I = DateTime.Parse(FechaI);
                        I = I.AddMonths(-1);
                        DateTime F = DateTime.Parse(FechaF);
                        ViewBag.Lista = db.Database.SqlQuery<spBuscarDetEmpleado_up_Result>
                                        ("spBuscarDetEmpleado_up @idEmpleado,@inicio,@fin",
                                        new SqlParameter("@idEmpleado", ID),
                                        new SqlParameter("@inicio", I),
                                        new SqlParameter("@fin", F));
                    }
                }
            
        return View();
        }
    }
}