using Microsoft.EntityFrameworkCore;


namespace CurrencyProject.Models
{
    public class CurrencyContext : DbContext
    {
        public CurrencyContext(DbContextOptions options) : base(options)
        {

        }
    }
}
