using CurrencyProject.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CurrencyProject.Data
{
    public class CurrencyDbContext : DbContext
    {
         public CurrencyDbContext() 
        {
        }
        public CurrencyDbContext(DbContextOptions<CurrencyDbContext> options)
            : base(options) 
        { 
        }

        public virtual DbSet<RootDTO> Roots { get; set; }
        public virtual DbSet<RateDTO> Rate { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-0LSIR8V;Initial Catalog=RatesDB;Integrated Security=True");
            }
        }
    }
}
