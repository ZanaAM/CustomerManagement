using CustomerManagement.API.BusinessLogic.Contracts;
using CustomerManagement.API.Controllers;
using CustomerManagement.API.Core.Models.Customer;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace CustomerManagement.API.Tests
{
    public class CustomerControllerTests
    {
        private readonly Mock<ICustomerService> _mockCustomerService;
        private readonly CustomerController _customerController;
        public CustomerControllerTests()
        {
            _mockCustomerService = new Mock<ICustomerService>(MockBehavior.Loose);
            _mockCustomerService = new Mock<ICustomerService>(MockBehavior.Loose);
            _customerController = new CustomerController(_mockCustomerService.Object, new NullLogger<CustomerController>());
        }
      
        [Fact]
        private async void CustomerController_ReturnsOK_WhenCreatingValidCustomer()
        {
            var mockRequest = MockObjects.CreateMockValidCustomer();
            var mockResponse = new CustomerDto { Id= 1};
            _mockCustomerService.Setup(service => service.CreateCustomerAsync(mockRequest))
                .ReturnsAsync(mockResponse);
            var result = await _customerController.PostCustomer(mockRequest);
            Assert.NotNull(result);

        }
    }
}