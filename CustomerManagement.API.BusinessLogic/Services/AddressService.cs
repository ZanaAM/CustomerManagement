using AutoMapper;
using CustomerManagement.API.BusinessLogic.Constants;
using CustomerManagement.API.BusinessLogic.Contracts;
using CustomerManagement.API.Core.Contracts;
using CustomerManagement.API.Core.Exceptions;
using CustomerManagement.API.Core.Models.Address;
using CustomerManagement.API.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.API.BusinessLogic.Services
{
    public class AddressService : IAddressService
    {
        private readonly ILogger _logger;
        private readonly IAddressRepository _addressRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        public AddressService(ILogger<AddressService> logger, IAddressRepository addressRepository, ICustomerRepository customerRepository, IMapper mapper)
        {
            _logger = logger;
            _addressRepository = addressRepository;
            _customerRepository = customerRepository;
            _mapper = mapper;
        }
        public async Task<AddressDto> GetAddressAsync(int id)
        {
            var address = await _addressRepository.GetAsync(id);

            if (address == null)
            {
                throw new NotFoundException(nameof(GetAddressAsync), id);
            }

            return _mapper.Map<AddressDto>(address);
        }
        
        public async Task<AddressDto?> CreateAddressAsync(CreateAddressDto createAddressDto)
        {
            try
            {
                var customer = await _customerRepository.GetAsync(createAddressDto.CustomerId);
                if (customer == null)
                {
                    throw new NotFoundException(nameof(_customerRepository.GetAsync), createAddressDto.CustomerId);
                }
                if (createAddressDto.IsPrimary)
                {
                    var primaryAddress = await _addressRepository.GetPrimaryAddressByCustomerId(createAddressDto.CustomerId);
                    if(primaryAddress != null)
                    {
                        throw new InvalidOperationException(ErrorMessages.PrimaryAddressExists);
                    }
                }
                var newAddress = _mapper.Map<Address>(createAddressDto);
                await _addressRepository.AddAsync(newAddress);
                return _mapper.Map<AddressDto>(newAddress);
            }
            catch (Exception exception)
            {
                _logger.LogError($"Failed to add an address for customer Id {createAddressDto.CustomerId} due to the error: {exception.Message}");
            }
            return null;
        }
        public async Task<string> UpdateAddressAsync(int id, UpdateAddressDto updateAddressDto)
        {
            var address = await _addressRepository.GetAsync(id);
            if (address == null)
            {
                throw new NotFoundException(nameof(_addressRepository.GetAsync), id);
            }
            if (address.IsPrimary && !updateAddressDto.IsPrimary)
            {
                return ErrorMessages.PrimaryAddressMandatory;
            }
            try
            {
                _mapper.Map(updateAddressDto, address);
                await FindAndUpdateExistingPrimaryAddress(address.CustomerId, false);
                await _addressRepository.UpdateAsync(address);
                return ResponseMessage.Success.ToString() ;
            }
            catch (Exception exception)
            {
                var errorMessage = $"Failed to update customer with id {id} due to the error: {exception.Data}";
                _logger.LogError(errorMessage);
                await FindAndUpdateExistingPrimaryAddress(address.CustomerId, true);
                return errorMessage ;
            }
            return ResponseMessage.Failure.ToString();

        }
        public async Task FindAndUpdateExistingPrimaryAddress(int customerId, Boolean isPrimary)
        {
            try
            {
                var primaryAddress = await _addressRepository.GetPrimaryAddressByCustomerId(customerId);
                if (primaryAddress != null )
                {
                    primaryAddress.IsPrimary = isPrimary;
                    await _addressRepository.UpdateAsync(primaryAddress);
                }
            }
            catch (Exception exception)
            {
                _logger.LogError($"Failed to update primary Address of Customer Id: {customerId} with status: {isPrimary} due to the error: {exception.Message}");
            }
        }
        public async Task<string> DeleteAddressAsync(int id)
        {
            try
            {
                var address = await _addressRepository.GetAsync(id);
                if (address == null)
                {
                    throw new NotFoundException(nameof(DeleteAddressAsync), id);
                }
                if (address.IsPrimary)
                {
                    return ErrorMessages.PrimaryAddressCannotBeRemoved;
                }
                var existingAddressCount = await _addressRepository.GetAllAddressByCustomerId(address.CustomerId);
                if(existingAddressCount == 1)
                {
                    throw new InvalidOperationException(ErrorMessages.AtleastOneAddressMandatory);
                }
                await _addressRepository.DeleteAsync(id);
                return ResponseMessage.Success.ToString();
            }
            catch (Exception exception)
            {
                _logger.LogError($"Failed to delete customer with id {id} due to the error: {exception.Message}");
            }
            return ResponseMessage.Failure.ToString();
        }
        public async Task<List<Address>> GetAllAddressAsync()
        {
            return await _addressRepository.GetAllAsync();
        }

    }
}
