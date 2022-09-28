using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Avia_kasi
{
    internal class AirportContext : DbContext
    {
        public DbSet<Airport> Airports { get; set; }

        public string DbPath { get; set; }

        public AirportContext()
        {
            var directory = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(directory);
            DbPath = Path.Join(path, "airports.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data Source={DbPath}");
    }
}
