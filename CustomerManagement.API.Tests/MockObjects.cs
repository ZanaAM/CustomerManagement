using CustomerManagement.API.Core.Models.Address;
using CustomerManagement.API.Core.Models.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.API.Tests
{
    public class MockObjects
    {
        public static CreateCustomerDto CreateMockValidCustomer()
        {
            return new CreateCustomerDto
            {
                Title = "Ms",
                Forename = "TestForeName",
                Surname = "TestSurname",
                EmailAddress = "test@gmail.com",
                MobileNumber = "1234555",
                Addresses = MockObjects.CreateMockPrimaryAddressList(true)
            };

        }
        public static List<AddressDto> CreateMockPrimaryAddressList(Boolean isPrimary)
        {
            List<AddressDto> addressDtos = new List<AddressDto>();
            addressDtos.Add(new AddressDto
            {
                AddressLineOne = "OneAddress",
                County = "Test",
                PostCode = "12344",
                Town = "TestTown",
                IsPrimary = isPrimary

            });
            return addressDtos;
        }
    }
}
