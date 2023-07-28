using CustomerManagement.API.Core.Models.Address;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.API.Core.Models.Customer
{
    public class CreateCustomerDto: BaseCustomerDto
    {
        public List<AddressDto> Addresses { get; set; }
    }
}
