

using CustomerManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CustomerManagement.Data
{
    public class CustomerManagementDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = (localdb)\\mssqllocaldb; Initial Catalog=CustomerManagementDb")
                .LogTo(Console.Out.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information );
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}
