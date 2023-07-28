using CustomerManagement.API.Core.Models.Address;
using CustomerManagement.API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.API.Core.Contracts
{
    public interface IAddressRepository : IGenericRepository<Address>
    {
        public Task<Address> GetPrimaryAddressByCustomerId(int customerId);
        public Task<int> GetAllAddressByCustomerId(int customerId);
    }
}
