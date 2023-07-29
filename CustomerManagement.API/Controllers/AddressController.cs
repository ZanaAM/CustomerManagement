using CustomerManagement.API.BusinessLogic.Constants;
using CustomerManagement.API.BusinessLogic.Contracts;
using CustomerManagement.API.Core.Models.Address;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CustomerManagement.API.Controllers
{
    [Produces("application/json")]
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
        [SwaggerResponse(200, "All addresses are successfully retrieved.", typeof(IEnumerable<AddressDto>))]
        public async Task<ActionResult<IEnumerable<AddressDto>>> GetAddresses()
        {
            var addresses = await _addressService.GetAllAddressAsync();
            return Ok(addresses);
        }


        // GET: api/Address/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IEnumerable<AddressDto>), 200)]
        [SwaggerOperation(Summary = "Get Address by Id")]
        [SwaggerResponse(200, "The address was successfully retrieved.", typeof(IEnumerable<AddressDto>))]
        [SwaggerResponse(404, "The address could not be found.")]
        public async Task<ActionResult<AddressDto>> GetAddress(int id)
        {
            var address = await _addressService.GetAddressAsync(id);
            return address != null? Ok(address): new ObjectResult(new { message = ErrorMessages.AddressNotExist })
            {
                StatusCode = StatusCodes.Status404NotFound,
            };
        }

        // PUT: api/Address/5
        [HttpPatch("{id}")]
        [SwaggerOperation(Summary = "Update primary status of address by Id")]
        [SwaggerResponse(200, "The primary status if address was successfully updated.")]
        [SwaggerResponse(400, "The address could not be updated.")]
        public async Task<IActionResult> PutAddress(int id, UpdateAddressDto updateAddressDto)
        {
            var updatedMessage = await _addressService.UpdateAddressAsync(id, updateAddressDto);
            return updatedMessage.Equals(ResponseMessage.Success.ToString()) ? Ok(updatedMessage) : new ObjectResult(new { message = updatedMessage })
            {
                StatusCode = StatusCodes.Status400BadRequest
            }; 
        }

        // POST: api/Address
        [HttpPost]
        [SwaggerOperation(Summary = "Add address")]
        [SwaggerResponse(201, "Theaddress status was successfully created.", typeof(IEnumerable<AddressDto>))]
        [SwaggerResponse(400, "The address could not be created.")]
        public async Task<ActionResult<AddressDto>> PostAddress(CreateAddressDto createAddressDto)
        {
            var address = await _addressService.CreateAddressAsync(createAddressDto);
            return address == null ? new ObjectResult(new { message = ErrorMessages.AddressNotCreated })
            {
                StatusCode = StatusCodes.Status400BadRequest
            } : CreatedAtAction(nameof(PostAddress), new { id = address.Id }, address);
        }

        // DELETE: api/Address/5
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete address by Id")]
        [SwaggerResponse(200, "The address was successfully deleted.")]
        [SwaggerResponse(400, "The address could not be deleted.")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            var deletedMessage = await _addressService.DeleteAddressAsync(id);
            return deletedMessage.Equals(ResponseMessage.Success)? Ok(deletedMessage): new ObjectResult(new { message = deletedMessage })
            {
                StatusCode = StatusCodes.Status400BadRequest
            };
        }
    }
}
