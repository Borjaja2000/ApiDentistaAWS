using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDentistaAWS.Models
{
    [Table("Tratamientos")]
    public class Tratamiento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("IdTratamiento")]
        public int IdTratamiento { get; set; }
        [Column("Nombre")]
        public String Nombre { get; set; }
        [Column("Precio")]
        public int Precio { get; set; }
        [Column("Detalles")]
        public String Detalles { get; set; }
        [Column("Descripcion")]
        public String Descripcion { get; set; }
        [Column("Dentista")]
        public String Dentista { get; set; }
        [Column("Imagen")]
        public String Imagen { get; set; }
        [Column("Duracion")]
        public String Duracion { get; set; }
    }
}
