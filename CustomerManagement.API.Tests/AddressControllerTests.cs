using CustomerManagement.API.BusinessLogic.Contracts;
using CustomerManagement.API.Controllers;
using CustomerManagement.API.Core.Models.Address;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.API.Tests
{
    public class AddressControllerTests
    {
        private readonly Mock<IAddressService> _mockAddressService;
        private readonly AddressController _addressController;
        public AddressControllerTests()
        {
            _mockAddressService = new Mock<IAddressService>(MockBehavior.Loose);

            _addressController = new AddressController(_mockAddressService.Object, new NullLogger<AddressController>());
        }

        [Fact]
        private async void AddressController_ReturnsOK_WhenCreatingValidCustomer()
        {
            var mockRequest = MockObjects.CreateMockCreateAddressRequest(false);
            var mockResponse = new AddressDto {Id = 1 };
            _mockAddressService.Setup(service => service.CreateAddressAsync(mockRequest))
                .ReturnsAsync(mockResponse);
            var result = await _addressController.PostAddress(mockRequest);
            Assert.NotNull(result);

        }
    }
}
