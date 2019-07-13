namespace Indicadores.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    // Agregamos libreria Linq para poder utilizar ToList
    using System.Linq;

    [Table("IndicadorPosvalor")]
    public partial class IndicadorPosvalor
    {
        [Key]
        public int IdIndPosvalor { get; set; }

        public int IdIndicador { get; set; }

        public int? IdPosvalor { get; set; }

        public int Orden { get; set; }

        public virtual Indicador Indicador { get; set; }

        public virtual Posvalor Posvalor { get; set; }

        public List<IndicadorPosvalor> ListarIndicadorPos()
        {
            //Creamos variable tipo List<Indicador>
            var indicador = new List<IndicadorPosvalor>();
            try
            {
                // Abrimos conexion
                using (var context = new contextoDatos())
                {
                    // Llenamos variable "indicador" con la lista de la tabla de la BD
                    indicador = context.IndicadorPosvalor.ToList();
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
