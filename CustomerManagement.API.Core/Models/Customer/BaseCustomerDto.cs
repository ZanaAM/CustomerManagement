using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.API.Core.Models.Customer
{
    public class BaseCustomerDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Forename { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string MobileNumber { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
