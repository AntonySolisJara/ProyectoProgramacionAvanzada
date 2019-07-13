namespace Indicadores.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    // Agregamos libreria Linq para poder utilizar ToList
    using System.Linq;

    [Table("Metadata")]
    public partial class Metadata
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Metadata()
        {
            Posvalor = new HashSet<Posvalor>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdMetadata { get; set; }

        [Key]
        [Required]
        [StringLength(3)]
        [RegularExpression(@"^[A-Z]{2}[_]*$", ErrorMessage = "Solo se pueden ingresar 2 letras Mayuscula y finalizar con guion bajo")]
        public string SiglasMet { get; set; }

        [Required]
        [StringLength(20)]
        public string NomMetadata { get; set; }

        [Required]
        [StringLength(50)]
        public string DescMetadata { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Posvalor> Posvalor { get; set; }

        // Creamos metodo para listar contenido de la BD
        public List<Metadata> ListarMetadata()
        {
            //Creamos variable tipo List<Unidad_Medida>
            var metadata = new List<Metadata>();
            try
            {
                // Abrimos conexion
                using (var context = new contextoDatos())
                {
                    // Llenamos variable "unidad" con la lista de la tabla de la BD
                    metadata = context.Metadata.ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return metadata;
        }
    }
}
