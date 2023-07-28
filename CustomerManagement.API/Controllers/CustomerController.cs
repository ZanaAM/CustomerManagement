using CustomerManagement.API.BusinessLogic.Constants;
using CustomerManagement.API.BusinessLogic.Contracts;
using CustomerManagement.API.Core.Models;
using CustomerManagement.API.Core.Models.Customer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CustomerManagement.API.Controllers
{
    [Produces("application/json")]
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
        [SwaggerResponse(200, "All customers are successfully retrieved.", typeof(IEnumerable<GetCustomerDto>))]
        public async Task<ActionResult<IEnumerable<GetCustomerDto>>> GetCustomers()
        {
            _logger.LogInformation("GetCustomers");
            var customers = await _customerService.GetAllCustomerAsync();
            _logger.LogInformation("Fetched all Customers");
            return Ok(customers);
        }
        // GET: api/Customer/Active
        [HttpGet("GetAllActive")]
        [SwaggerResponse(200, "All active customers are successfully retrieved.", typeof(IEnumerable<GetCustomerDto>))]
        public async Task<ActionResult<IEnumerable<GetCustomerDto>>> GetActiveCustomers()
        {
            _logger.LogInformation("GetActiveCustomers");
            var customers = await _customerService.GetAllActiveCustomerAsync();
            _logger.LogInformation("Fetched all active Customers");
            return Ok(customers);
        }
        // GET: api/Customer/?StartIndex=0&pagesize=25&PageNumber=1
        [HttpGet]
        [SwaggerResponse(200, "All customers are successfully retrieved.")]
        public async Task<ActionResult<PaginatedResult<GetCustomerDto>>> GetPagedCustomers([FromQuery] QueryParameters queryParameters)
        {
            _logger.LogInformation("GetPagedCountries");
            var pagedCustomersResult = await _customerService.GetAllPaginatedAsync(queryParameters);
            _logger.LogInformation("Fetched all Customers by pagination");
            return Ok(pagedCustomersResult);
        }

        // GET: api/Customer/5
        [ProducesResponseType(typeof(IEnumerable<CustomerDto>), 200)]
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get customer by Id")]
        [SwaggerResponse(200, "The customer was successfully retrieved.", typeof(IEnumerable<GetCustomerDto>))]
        [SwaggerResponse(404, "The customer could not be found.")]
        public async Task<ActionResult<CustomerDto>> GetCustomer(int id)
        {
            _logger.LogInformation("GetCustomer");
            var customer = await _customerService.GetCustomerAsync(id);
            _logger.LogInformation("Fetched Customer");
            return customer != null? Ok(customer): new ObjectResult(new { message = ErrorMessages.CustomerDoesNotExist })
            {
                StatusCode = StatusCodes.Status404NotFound,
            };
        }

        // PUT: api/Customer/5
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update customer status by Id")]
        [SwaggerResponse(200, "The customer status was successfully updated.", typeof(IEnumerable<GetCustomerDto>))]
        [SwaggerResponse(400, "The customer could not be updated.")]
        public async Task<IActionResult> PutCustomer(int id, UpdateCustomerDto updateCountryDto)
        {
            _logger.LogInformation("PutCustomer");
            var updatedMessage = await _customerService.UpdateCustomerAsync(id, updateCountryDto);
            _logger.LogInformation("Completed Customer update");
            return updatedMessage.Equals(ResponseMessage.Success.ToString()) ? Ok(updatedMessage) : new ObjectResult(new { message = updatedMessage })
            {
                StatusCode = StatusCodes.Status400BadRequest,
            };
        }

        // POST: api/Customer
        [HttpPost]
        [SwaggerOperation(Summary = "Update customer status by Id")]
        [SwaggerResponse(201, "The customer status was successfully updated.", typeof(IEnumerable<GetCustomerDto>))]
        [SwaggerResponse(400, "The customer could not be created.")]
        public async Task<ActionResult<CustomerDto>> PostCustomer([FromBody]CreateCustomerDto createCustomerDto)
        {
            _logger.LogInformation("PostCustomer");
            var customer = await _customerService.CreateCustomerAsync(createCustomerDto);
            _logger.LogInformation("Completed Customer creation");
            return customer == null? new ObjectResult(new { message = ErrorMessages.CustomerNotCreated })
            {
                StatusCode = StatusCodes.Status400BadRequest,
            } 
            : CreatedAtAction(nameof(PostCustomer), new { id = customer.Id}, customer);
        }

        // DELETE: api/Customer/5
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete customer by Id")]
        [SwaggerResponse(200, "The customer was successfully deleted.", typeof(IEnumerable<GetCustomerDto>))]
        [SwaggerResponse(400, "The customer could not be deleted.")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            _logger.LogInformation("DeleteCustomer");
            var deletedMessage = await _customerService.DeleteCustomerAsync(id);
            _logger.LogInformation("Completed Customer deletion");
            return deletedMessage.Equals(ResponseMessage.Success.ToString())? Ok(deletedMessage): new ObjectResult(new { message = deletedMessage})
            {
                StatusCode = StatusCodes.Status400BadRequest,
            };
        }
    }
}
