using CustomerManagement.API.BusinessLogic.Contracts;
using CustomerManagement.API.BusinessLogic.Services;
using CustomerManagement.API.Core.Contracts;
using CustomerManagement.API.Core.Repository;

namespace CustomerManagement.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        }
    }
}
