namespace Indicadores.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Periodo")]
    public partial class Periodo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Periodo()
        {
            Indicador = new HashSet<Indicador>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPeriodo { get; set; }

        [Key]
        [StringLength(1)]
        public string SiglasPer { get; set; }

        [Required]
        [StringLength(50)]
        public string DescPeriodo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Indicador> Indicador { get; set; }
    }
}
