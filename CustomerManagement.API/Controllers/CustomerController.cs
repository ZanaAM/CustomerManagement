using CustomerManagement.API.BusinessLogic.Constants;
using CustomerManagement.API.BusinessLogic.Contracts;
using CustomerManagement.API.Core.Models;
using CustomerManagement.API.Core.Models.Customer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger _logger;

        public CustomerController(ICustomerService customerService, ILogger<CustomerController> logger)
        {
            _customerService = customerService;
            _logger = logger;
        }

        // GET: api/Customer/GetAll
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<GetCustomerDto>>> GetCustomers()
        {
            _logger.LogInformation("GetCustomers");
            var customers = await _customerService.GetAllCustomerAsync();
            _logger.LogInformation("Fetched all Customers");
            return Ok(customers);
        }
        // GET: api/Customer/Active
        [HttpGet("GetAllActive")]
        public async Task<ActionResult<IEnumerable<GetCustomerDto>>> GetActiveCustomers()
        {
            _logger.LogInformation("GetActiveCustomers");
            var customers = await _customerService.GetAllActiveCustomerAsync();
            _logger.LogInformation("Fetched all active Customers");
            return Ok(customers);
        }
        // GET: api/Customer/?StartIndex=0&pagesize=25&PageNumber=1
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<GetCustomerDto>>> GetPagedCustomers([FromQuery] QueryParameters queryParameters)
        {
            _logger.LogInformation("GetPagedCountries");
            var pagedCustomersResult = await _customerService.GetAllPaginatedAsync(queryParameters);
            _logger.LogInformation("Fetched all Customers by pagination");
            return Ok(pagedCustomersResult);
        }

        // GET: api/Customer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomer(int id)
        {
            _logger.LogInformation("GetCustomer");
            var customer = await _customerService.GetCustomerAsync(id);
            _logger.LogInformation("Fetched Customer");
            return Ok(customer);
        }

        // PUT: api/Customer/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, UpdateCustomerDto updateCountryDto)
        {
            _logger.LogInformation("PutCustomer");
            var updatedMessage = await _customerService.UpdateCustomerAsync(id, updateCountryDto);
            _logger.LogInformation("Completed Customer update");
            return updatedMessage.Equals(ResponseMessage.Success.ToString()) ? Ok(updatedMessage) : new ObjectResult(new { message = updatedMessage })
            {
                StatusCode = StatusCodes.Status500InternalServerError,
            };
        }

        // POST: api/Customer
        [HttpPost]
        public async Task<ActionResult<CustomerDto>> PostCustomer([FromBody]CreateCustomerDto createCustomerDto)
        {
            _logger.LogInformation("PostCustomer");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var customer = await _customerService.CreateCustomerAsync(createCustomerDto);
            _logger.LogInformation("Completed Customer creation");
            return customer == null? StatusCode(500) : CreatedAtAction(nameof(PostCustomer), new { id = customer.Id}, customer);
        }

        // DELETE: api/Customer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            _logger.LogInformation("DeleteCustomer");
            await _customerService.DeleteCustomerAsync(id);
            _logger.LogInformation("Completed Customer deletion");
            return NoContent();
        }
    }
}
