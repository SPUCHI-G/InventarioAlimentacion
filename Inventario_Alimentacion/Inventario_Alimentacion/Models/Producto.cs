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
    
    public partial class Producto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Producto()
        {
            this.Detalle_Salida = new HashSet<Detalle_Salida>();
            this.Temporal = new HashSet<Temporal>();
            this.Temporal2 = new HashSet<Temporal2>();
        }
    
        public long ID_Producto { get; set; }
        public int fk_Categoria { get; set; }
        public int fk_Subcategoria { get; set; }
        public int fk_NomProducto { get; set; }
    
        public virtual Categoria Categoria { get; set; }
        public virtual Subcategoria Subcategoria { get; set; }
        public virtual Nom_Producto Nom_Producto { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Detalle_Salida> Detalle_Salida { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Temporal> Temporal { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Temporal2> Temporal2 { get; set; }
    }
}
