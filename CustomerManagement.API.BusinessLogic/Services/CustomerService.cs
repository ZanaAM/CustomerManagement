using AutoMapper;
using CustomerManagement.API.BusinessLogic.Constants;
using CustomerManagement.API.BusinessLogic.Contracts;
using CustomerManagement.API.Core.Contracts;
using CustomerManagement.API.Core.Exceptions;
using CustomerManagement.API.Core.Models;
using CustomerManagement.API.Core.Models.Customer;
using CustomerManagement.API.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.API.BusinessLogic.Services
{
    public class CustomerService: ICustomerService
    {
        private readonly ILogger _logger;
        private readonly ICustomerRepository _customersRepository;
        private readonly IAddressRepository _addressesRepository;
        private readonly IMapper _mapper;
      public CustomerService(ILogger<CustomerService> logger, 
          ICustomerRepository customersRepository, IAddressRepository addressesRepository, IMapper mapper)
        {
            _logger = logger;
            _customersRepository = customersRepository;
            _addressesRepository = addressesRepository;
            _mapper = mapper;
        }
        public async Task<GetCustomerDto> GetCustomerAsync(int id)
        {
            var customer = await _customersRepository.GetDetails(id);

            if (customer == null)
            {
                throw new NotFoundException(nameof(GetCustomerAsync), id);
            }

            return _mapper.Map<GetCustomerDto>(customer);
        }
        public async Task<CustomerDto?> CreateCustomerAsync(CreateCustomerDto createCustomerDto)
        { 
            try
            {

                bool doesCustomerExists = await _customersRepository.ExistsByEmailAddress(createCustomerDto.EmailAddress);
                if (doesCustomerExists)
                {
                    throw new InvalidOperationException(ErrorMessages.CustomerIsNotUnique);
                }
                if (createCustomerDto.Addresses == null || !createCustomerDto.Addresses.Any())
                {
                    throw new BadRequestException(ErrorMessages.MandatoryAddressMissing);
                }
                if (!createCustomerDto.Addresses.Any(a => a.IsPrimary))
                {
                    throw new BadRequestException(ErrorMessages.PrimaryAddressMandatory);
                }
                var newCustomer = _mapper.Map<Customer>(createCustomerDto);
                newCustomer.CreatedDate = DateTime.Now;
                await _customersRepository.AddAsync(newCustomer);
                return _mapper.Map<CustomerDto>(newCustomer);
            }
            catch(Exception exception)
            {
                _logger.LogError($"Failed to create a customer with Forename {createCustomerDto.Forename} due to the error: {exception.Message}");
            }
            return null;
        }
        public async Task<string> UpdateCustomerAsync(int id, UpdateCustomerDto updateCustomerDto)
        {
            var customer = await _customersRepository.GetAsync(id);
            if (customer == null)
            {
                throw new NotFoundException(nameof(GetCustomerAsync), id);
            }
            try
            {
                _mapper.Map(updateCustomerDto, customer);
                await _customersRepository.UpdateAsync(customer);
                return ResponseMessage.Success.ToString();
             }
            catch (Exception exception)
            {
                var errorMessage = $"Failed to update customer with id {id} due to the error: {exception.Message}";
                _logger.LogError(errorMessage);
                return errorMessage;
            }
            return ResponseMessage.Failure.ToString();
            
        }
        public async Task<string> DeleteCustomerAsync(int id)
        {
            try
            {
                var customer = await _customersRepository.GetAsync(id);
                if (customer == null)
                {
                    throw new NotFoundException(nameof(GetCustomerAsync), id);
                }

                await _customersRepository.DeleteAsync(id);
                return ResponseMessage.Success.ToString();
            }
            catch (Exception exception)
            {
                var errorMessage = $"Failed to delete customer with id {id} due to the error: {exception.Message}";
                _logger.LogError(errorMessage);
            }
            return ResponseMessage.Failure.ToString();
        }
        public async Task<List<GetCustomerDto>> GetAllCustomerAsync()
        {
            var customers =  await _customersRepository.GetAllAsync();
            return _mapper.Map<List<GetCustomerDto>>(customers);
        }
        public async Task<List<GetCustomerDto>> GetAllActiveCustomerAsync()
        {
            var customers = await _customersRepository.GetAllActive();
            return _mapper.Map<List<GetCustomerDto>>(customers);
        }
        public async Task<PaginatedResult<GetCustomerDto>> GetAllPaginatedAsync(QueryParameters queryParameters)
        {
            return await _customersRepository.GetAllAsync<GetCustomerDto>(queryParameters);
        }
    }
}
