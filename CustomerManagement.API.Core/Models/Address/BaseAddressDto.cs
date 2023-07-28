using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.API.Core.Models.Address
{
    public class BaseAddressDto
    {
       [Required]
       public string AddressLineOne { get; set; }
       public string AddressLineTwo { get; set; }
       [Required]
       public string Town { get; set; }
       public string? County { get; set; }
       [Required]
       public string PostCode { get; set; }
       public string Country { get; set; }
       [Range(1, int.MaxValue)]
       public int CustomerId { get; set; }
       [Required]
       public bool IsPrimary { get; set; }

    }
}
