using Microsoft.EntityFrameworkCore;

namespace Callendar.Data
{
    public class CallendarDataContext : DbContext
    {
        public CallendarDataContext(DbContextOptions<CallendarDataContext> options):
            base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
        }

        // create the tables of the database
        public DbSet<User> User { get; set; }
        public DbSet<Event> Event { get; set; }
    }
}