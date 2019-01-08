using System.Data.Entity;

namespace Weddy.Models
{
    public class WeddyDbContext : DbContext
    {
        public  WeddyDbContext()
        {
        }

        public WeddyDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }

        public DbSet<Pool> Pools { get; set; }
    }
}