namespace Indicadores.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    // Agregamos libreria Linq para poder utilizar ToList
    using System.Linq;
    using OfficeOpenXml;
    using System.IO;
    using System.Xml.Linq;

    [Table("UnidadMedida")]
    public partial class UnidadMedida
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UnidadMedida()
        {
            Indicador = new HashSet<Indicador>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUnidad { get; set; }

        [Key]
        [Required]
        [StringLength(3)]
        [RegularExpression(@"^[A-Z]*$", ErrorMessage = "Solo se pueden ingresar 3 letras Mayuscula")]
        public string SiglasUni { get; set; }

        [Required]
        [StringLength(50)]
        public string DescUnidad { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Indicador> Indicador { get; set; }

        // Creamos metodo para listar contenido de la BD
        public List<UnidadMedida> ListarUnidad()
        {
            //Creamos variable tipo List<Unidad_Medida>
            var unidad = new List<UnidadMedida>();
            try
            {
                // Abrimos conexion
                using (var context = new contextoDatos())
                {
                    // Llenamos variable "unidad" con la lista de la tabla de la BD
                    unidad = context.UnidadMedida.ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return unidad;
        }

    }
}
