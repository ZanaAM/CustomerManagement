using CustomerManagement.API.Core.Models.Customer;
using CustomerManagement.API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.API.Core.Contracts
{
    public interface ICustomerRepository: IGenericRepository<Customer>
    {
        Task<CustomerDto> GetDetails(int id);
        Task<bool> ExistsByEmailAddress(string emailAddress);
        Task<List<Customer>> GetAllActive();
    }
}
