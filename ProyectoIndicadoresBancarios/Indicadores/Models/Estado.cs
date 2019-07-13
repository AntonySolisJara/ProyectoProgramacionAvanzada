namespace Indicadores.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Estado")]
    public partial class Estado
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Estado()
        {
            Indicador = new HashSet<Indicador>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEstado { get; set; }

        [Key]
        [StringLength(4)]
        [RegularExpression(@"^[A-Z]*$", ErrorMessage = "Solo se pueden ingresar 4 letras Mayuscula")]
        public string SiglasEst { get; set; }

        [Required]
        [StringLength(50)]
        public string DescEstado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Indicador> Indicador { get; set; }
    }
}
