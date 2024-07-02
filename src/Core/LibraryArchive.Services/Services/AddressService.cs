using AutoMapper;
using LibraryArchive.Data.Entities;
using LibraryArchive.Services.DTOs.Address;
using LibraryArchive.Services.Repositories.Interfaces;
using LibraryArchive.Services.Units;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryArchive.Services
{
    public class AddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IUnitOfWork _unitOfWork; // UnitOfWork tanımı eklendi
        private readonly IMapper _mapper;
        private readonly ILogger<AddressService> _logger;

        public AddressService(IAddressRepository addressRepository, IUnitOfWork unitOfWork, IMapper mapper, ILogger<AddressService> logger)
        {
            _addressRepository = addressRepository;
            _unitOfWork = unitOfWork; // UnitOfWork constructor'a eklendi
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<AddressReadDto>> GetAddressesByUserIdAsync(string userId)
        {
            var addresses = await _addressRepository.GetAddressesByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<AddressReadDto>>(addresses);
        }

        public async Task<AddressReadDto> GetAddressByIdAsync(string userId, int addressId)
        {
            var address = await _addressRepository.GetAddressByIdAsync(userId, addressId);
            return address != null ? _mapper.Map<AddressReadDto>(address) : null;
        }

        public async Task<AddressReadDto> AddAddressAsync(AddressCreateDto addressDto, string userId)
        {
            var address = new Address
            {
                UserId = userId,
                Street = addressDto.Street,
                City = addressDto.City,
                State = addressDto.State,
                PostalCode = addressDto.PostalCode,
                Country = addressDto.Country
            };

            await _addressRepository.AddAddressAsync(address);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<AddressReadDto>(address);
        }

        public async Task<bool> UpdateAddressAsync(string userId, AddressUpdateDto addressDto)
        {
            var address = await _addressRepository.GetAddressByIdAsync(userId, addressDto.AddressId);
            if (address != null)
            {
                _mapper.Map(addressDto, address);
                await _addressRepository.UpdateAddressAsync(address);
                await _unitOfWork.CompleteAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAddressAsync(string userId, int addressId)
        {
            var address = await _addressRepository.GetAddressByIdAsync(userId, addressId);
            if (address != null)
            {
                await _addressRepository.DeleteAddressAsync(addressId);
                await _unitOfWork.CompleteAsync();
                return true;
            }
            return false;
        }
    }
}
