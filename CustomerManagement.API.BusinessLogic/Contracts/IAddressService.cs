using CustomerManagement.API.Core.Models;
using CustomerManagement.API.Core.Models.Address;
using CustomerManagement.API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.API.BusinessLogic.Contracts
{
    public interface IAddressService
    {
        Task<AddressDto?> CreateAddressAsync(CreateAddressDto createAddressDto);
        Task<string> UpdateAddressAsync(int id, UpdateAddressDto updateAddressDto);
        Task<List<Address>> GetAllAddressAsync();
        Task<string> DeleteAddressAsync(int id);
        Task<AddressDto> GetAddressAsync(int id);
    }
}
