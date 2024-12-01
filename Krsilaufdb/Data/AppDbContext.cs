using Kreislauf.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kreislauf.Data
{
    public class AppDbContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseMySql(Connections.sqlConStr, new MySqlServerVersion(new Version(8, 0, 21)));
        }

        public DbSet<Person> Personen { get; set; }
        public DbSet<Klasse> Klassen { get; set; }
        public DbSet<Schule> Schulen { get; set; }

        public DbSet<Barcode> Barcodes { get; set; }


      

    }
}
