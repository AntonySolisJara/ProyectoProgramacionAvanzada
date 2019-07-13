namespace Indicadores.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("CuentaCatalogo")]
    public partial class CuentaCatalogo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CuentaCatalogo()
        {
            Indicador = new HashSet<Indicador>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCtaCatalogo { get; set; }

        [Key]
        [Required]
        [StringLength(15)]
        [RegularExpression(@"^[0-9a-zA-Z]+$/*$", ErrorMessage = "No se permiten caracteres especiales ni espacios vacios")]
        public string CtaCatalogo { get; set; }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Indicador> Indicador { get; set; }

        public List<CuentaCatalogo> ListarCuenta()
        {
            var cuenta = new List<CuentaCatalogo>();
            try
            {
                using (var context = new contextoDatos())
                {
                    cuenta = context.CuentaCatalogo.ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return cuenta;
        }
    }
}
