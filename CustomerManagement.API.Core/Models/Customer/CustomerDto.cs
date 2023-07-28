using CustomerManagement.API.Core.Models.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.API.Core.Models.Customer
{
    public class CustomerDto: BaseCustomerDto, IBaseDto
    {
        public int Id { get; set; }
        public List<AddressDto> Addresses { get; set; }
    }
}
