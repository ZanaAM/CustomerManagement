using CustomerManagement.API.BusinessLogic.Constants;
using CustomerManagement.API.BusinessLogic.Contracts;
using CustomerManagement.API.Core.Models.Address;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        private readonly ILogger _logger;

        public AddressController(IAddressService addressService, ILogger<AddressController> logger)
        {
            _addressService = addressService;
            _logger = logger;
        }

        // GET: api/Address/GetAll
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<AddressDto>>> GetAddresses()
        {
            var addresses = await _addressService.GetAllAddressAsync();
            return Ok(addresses);
        }


        // GET: api/Address/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AddressDto>> GetAddress(int id)
        {
            var address = await _addressService.GetAddressAsync(id);
            return Ok(address);
        }

        // PUT: api/Address/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddress(int id, UpdateAddressDto updateAddressDto)
        {
            var updatedMessage = await _addressService.UpdateAddressAsync(id, updateAddressDto);
            return updatedMessage.Equals(ResponseMessage.Success.ToString()) ? Ok(updatedMessage) : new ObjectResult(new { message = updatedMessage })
            {
                StatusCode = StatusCodes.Status500InternalServerError,
            }; 
        }

        // POST: api/Address
        [HttpPost]
        public async Task<ActionResult<AddressDto>> PostAddress(CreateAddressDto createAddressDto)
        {
            var address = await _addressService.CreateAddressAsync(createAddressDto);
            return address == null ? StatusCode(500) : CreatedAtAction(nameof(PostAddress), new { id = address.Id }, address);
        }

        // DELETE: api/Address/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            var deletedMessage = await _addressService.DeleteAddressAsync(id);
            return deletedMessage.Equals(ResponseMessage.Success)? Ok(deletedMessage): new ObjectResult(new { message = deletedMessage })
            {
                StatusCode = StatusCodes.Status500InternalServerError,
            };
        }
    }
}
