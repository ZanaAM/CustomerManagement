using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.API.Data
{
    public class CustomerManagementDbContext :DbContext
    {
        public CustomerManagementDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }

    //public class CustomerManagementDbContextFactory : IDesignTimeDbContextFactory<CustomerManagementDbContext>
    //{
    //    public CustomerManagementDbContext CreateDbContext(string[] args)
    //    {
    //        IConfiguration config = new ConfigurationBuilder()
    //            .SetBasePath(Directory.GetCurrentDirectory())
    //            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    //            .Build();

    //        var optionsBuilder = new DbContextOptionsBuilder<CustomerManagementDbContext>();
    //        var connectionString = config.GetConnectionString("CustomerManagementDbConnectionString");
    //        optionsBuilder.UseSqlServer(connectionString);
    //        return new CustomerManagementDbContext(optionsBuilder.Options);
    //    }
    //}
}
