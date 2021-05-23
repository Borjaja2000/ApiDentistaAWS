using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDentistaAWS.Models
{
    [Table("Clientes")]
    public class Cliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Id")]
        public int IdCliente { get; set; }
        [Column("Usuario")]
        public String Usuario { get; set; }
        [Column("Pass")]
        public String Password { get; set; }
        [Column("Nombre")]
        public String Nombre { get; set; }
        [Column("Apellido")]
        public String Apellido { get; set; }
        [Column("Domicilio")]
        public String Domicilio { get; set; }
        [Column("Edad")]
        public int Edad { get; set; }
        [Column("Dni")]
        public String Dni { get; set; }
        [Column("Telefono")]
        public String Telefono { get; set; }
       


    }
}
