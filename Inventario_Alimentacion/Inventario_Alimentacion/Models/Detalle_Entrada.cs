//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Inventario_Alimentacion.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Detalle_Entrada
    {
        public long ID_Det_entr { get; set; }
        public Nullable<long> fk_Entrada { get; set; }
        public Nullable<int> fk_TipoEntrada { get; set; }
        public Nullable<int> fk_Proveedor { get; set; }
        public Nullable<int> fk_Donante { get; set; }
        public Nullable<long> fk_Producto { get; set; }
        public Nullable<decimal> Ent_Cantidad_Peso { get; set; }
        public string Ent_Factura { get; set; }
        public Nullable<decimal> Ent_Precio_Unitario { get; set; }
        public Nullable<decimal> Ent_Precio_Total { get; set; }
        public Nullable<decimal> Ent_iva { get; set; }
        public Nullable<decimal> Ent_Descuento { get; set; }
        public Nullable<decimal> Ent_Temperatura { get; set; }
        public Nullable<System.DateTime> Ent_FechVencim { get; set; }
        public Nullable<bool> Dispo_stock { get; set; }
    }
}