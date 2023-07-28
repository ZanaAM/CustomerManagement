using CustomerManagement.API.Core.Models;
using CustomerManagement.API.Core.Models.Customer;
using CustomerManagement.API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.API.BusinessLogic.Contracts
{
    public interface ICustomerService
    {
        Task<CustomerDto?> CreateCustomerAsync(CreateCustomerDto createCustomerDto);
        Task<string> UpdateCustomerAsync(int id, UpdateCustomerDto updateCustomerDto);
        Task<List<GetCustomerDto>> GetAllCustomerAsync();
        Task<List<GetCustomerDto>> GetAllActiveCustomerAsync();
        Task<string> DeleteCustomerAsync(int id);
        Task<GetCustomerDto> GetCustomerAsync(int id);
        Task<PaginatedResult<GetCustomerDto>> GetAllPaginatedAsync(QueryParameters queryParameters);
    }
}
