using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.API.Data
{
    public class Address 
    {
        public int Id { get; set; }
        public string AddressLineOne { get; set; }
        public string? AddressLineTwo { get; set; }
        public string Town { get; set; }
        public string? County { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }
        [ForeignKey(nameof(CustomerId))]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public bool IsPrimary { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
