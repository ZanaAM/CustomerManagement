﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.API.Core.Models.Address
{
    public class CreateAddressDto: BaseAddressDto
    {
        [Range(1, int.MaxValue)]
        public int CustomerId { get; set; }
    }
}
