﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.API.Core.Models.Customer
{
    public class GetCustomerDto: BaseCustomerDto, IBaseDto
    {
        public int Id { get; set; }
    }
}
