namespace Indicadores.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    // Agregamos libreria Linq para poder utilizar ToList
    using System.Linq;

    [Table("Posvalor")]
    public partial class Posvalor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Posvalor()
        {
            IndicadorPosvalor = new HashSet<IndicadorPosvalor>();
        }

        [Key]
        public int IdPosvalor { get; set; }

        [Required]
        [StringLength(3)]
        [RegularExpression(@"^[A-Z]{2}[_]*$", ErrorMessage = "Solo se pueden ingresar 2 letras Mayuscula y finalizar con guion bajo")]
        public string SiglasMet { get; set; }

        [Required]
        [StringLength(3)]
        [RegularExpression(@"^[0-9a-zA-Z]+$/*$", ErrorMessage = "No se permiten caracteres especiales ni espacios vacios")]
        public string SiglasPos { get; set; }

        [Required]
        [StringLength(20)]
        public string NomPosvalor { get; set; }

        [Required]
        [StringLength(50)]
        public string DescPosvalor { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IndicadorPosvalor> IndicadorPosvalor { get; set; }

        public virtual Metadata Metadata { get; set; }

        // Creamos metodo para listar contenido de la BD
        public List<Posvalor> ListarPosvalor()
        {
            //Creamos variable tipo List<Posvalor>
            var posvalor = new List<Posvalor>();
            try
            {
                // Abrimos conexion
                using (var context = new contextoDatos())
                {
                    // Llenamos variable "posvalor" con la lista de la tabla de la BD
                    posvalor = context.Posvalor.ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return posvalor;
        }

        [NotMapped]
        public List<Metadata> listaMetadata { get; set; }
    }
}
