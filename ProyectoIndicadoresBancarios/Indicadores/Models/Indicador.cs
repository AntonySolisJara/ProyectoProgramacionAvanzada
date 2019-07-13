namespace Indicadores.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    // Agregamos libreria Linq para poder utilizar ToList
    using System.Linq;

    [Table("Indicador")]
    public partial class Indicador
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Indicador()
        {
            IndicadorPosvalor = new HashSet<IndicadorPosvalor>();
        }

        [Key]
        public int IdIndicador { get; set; }

        [Required]
        [StringLength(15)]
        [RegularExpression(@"^[0-9a-zA-Z]+$/*$", ErrorMessage = "No se permiten caracteres especiales ni espacios vacios")]
        public string CtaCatalogo { get; set; }

        [Required]
        [StringLength(4)]
        public string SiglasEst { get; set; }

        [Required]
        [StringLength(1)]
        public string SiglasPer { get; set; }

        [Required]
        [StringLength(3)]
        public string SiglasUni { get; set; }

        public virtual CuentaCatalogo CuentaCatalogo { get; set; }

        public virtual Estado Estado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IndicadorPosvalor> IndicadorPosvalor { get; set; }

        public virtual Periodo Periodo { get; set; }

        public virtual UnidadMedida UnidadMedida { get; set; }

        // Creamos metodo para listar contenido de la BD
        public List<Indicador> ListarIndicador()
        {
            //Creamos variable tipo List<Indicador>
            var indicador = new List<Indicador>();
            try
            {
                // Abrimos conexion
                using (var context = new contextoDatos())
                {
                    // Llenamos variable "indicador" con la lista de la tabla de la BD
                    indicador = context.Indicador.ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return indicador;
        }
    }
}
