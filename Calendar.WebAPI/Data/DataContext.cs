using Calendar.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Calendar.WebAPI.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Availability> Availabilities { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
        }
    }
}