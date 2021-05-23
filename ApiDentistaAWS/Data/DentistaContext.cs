using Microsoft.EntityFrameworkCore;
using ApiDentistaAWS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDentistaAWS.Data
{
    public class DentistaContext : DbContext
    {
        public DentistaContext(DbContextOptions<DentistaContext> options)
            : base(options) { }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Tratamiento> Tratamientos { get; set; }

    }
}
