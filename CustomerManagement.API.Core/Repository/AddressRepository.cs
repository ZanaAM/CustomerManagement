using AutoMapper;
using AutoMapper.QueryableExtensions;
using CustomerManagement.API.Core.Contracts;
using CustomerManagement.API.Core.Models.Address;
using CustomerManagement.API.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.API.Core.Repository
{
    public class AddressRepository:GenericRepository<Address>, IAddressRepository
    {
        private readonly CustomerManagementDbContext _context;
        private readonly IMapper _mapper;
        public AddressRepository(CustomerManagementDbContext context, IMapper mapper) : base(context, mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }
        public async Task<Address?> GetPrimaryAddressByCustomerId(int customerId)
        {
            return await _context.Addresses
                .FirstOrDefaultAsync(q => q.CustomerId == customerId && q.IsPrimary);
        }
        public async Task<int> GetAllAddressByCustomerId(int customerId)
        {
            return await _context.Addresses.CountAsync(address => address.CustomerId == customerId);


        }
    }
}
