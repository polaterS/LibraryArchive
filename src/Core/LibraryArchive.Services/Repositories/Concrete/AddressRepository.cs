using LibraryArchive.Data.Context;
using LibraryArchive.Data.Entities;
using LibraryArchive.Services.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryArchive.Services.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly LibraryArchiveContext _context;

        public AddressRepository(LibraryArchiveContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Address>> GetAddressesByUserIdAsync(string userId)
        {
            return await _context.Addresses.Where(a => a.UserId == userId).ToListAsync();
        }



        public async Task<Address> GetAddressByIdAsync(string userId, int addressId)
        {
            return await _context.Addresses
                .FirstOrDefaultAsync(a => a.UserId == userId && a.AddressId == addressId);
        }

        public async Task<Address> AddAddressAsync(Address address)
        {
            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();
            return address;
        }

        public async System.Threading.Tasks.Task UpdateAddressAsync(Address address)
        {
            _context.Addresses.Update(address);
            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task DeleteAddressAsync(int addressId)
        {
            var address = await _context.Addresses.FindAsync(addressId);
            if (address != null)
            {
                _context.Addresses.Remove(address);
                await _context.SaveChangesAsync();
            }
        }
    }
}
