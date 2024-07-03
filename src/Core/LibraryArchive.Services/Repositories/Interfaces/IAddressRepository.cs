using LibraryArchive.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryArchive.Services.Repositories.Interfaces
{
    public interface IAddressRepository
    {
        Task<IEnumerable<Address>> GetAddressesByUserIdAsync(string userId);
        Task<Address> GetAddressByIdAsync(string userId, int addressId);
        Task<Address> AddAddressAsync(Address address);
        System.Threading.Tasks.Task UpdateAddressAsync(Address address);
        System.Threading.Tasks.Task DeleteAddressAsync(int addressId);
        Task<Address> GetDefaultAddressByUserIdAsync(string userId);
    }
}
