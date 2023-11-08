using Microsoft.EntityFrameworkCore;
using PantAPIDreamsStyle.models.pant;

namespace PantAPIDreamsStyle.data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Pant> Pants { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}
