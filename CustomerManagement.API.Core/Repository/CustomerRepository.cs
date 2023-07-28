using AutoMapper;
using AutoMapper.QueryableExtensions;
using CustomerManagement.API.Core.Contracts;
using CustomerManagement.API.Core.Exceptions;
using CustomerManagement.API.Core.Models.Customer;
using CustomerManagement.API.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.API.Core.Repository
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        private readonly CustomerManagementDbContext _context;
        private readonly IMapper _mapper;

        public CustomerRepository(CustomerManagementDbContext context, IMapper mapper) : base(context, mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<CustomerDto> GetDetails(int id)
        {
            var customer = await _context.Customers.Include(q => q.Addresses)
                .ProjectTo<CustomerDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(q => q.Id == id);

            if (customer == null)
            {
                throw new NotFoundException(nameof(GetDetails), id);
            }

            return customer;
        }
        public async Task<bool> ExistsByEmailAddress(string emailAddress)
        {
            var customer = await _context.Customers
                .ProjectTo<CustomerDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(q => q.EmailAddress == emailAddress);
            return customer != null;
        }

        public async Task<List<Customer>> GetAllActive()
        {
            return await _context.Customers
                .Where(c => c.IsActive)
                .ToListAsync();
        }
    }
}
