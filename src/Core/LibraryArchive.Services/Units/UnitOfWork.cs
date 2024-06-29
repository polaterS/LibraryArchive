using LibraryArchive.Data.Context;
using LibraryArchive.Services.Repositories.Concrete;
using LibraryArchive.Services.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace LibraryArchive.Services.Units
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly LibraryArchiveContext _context;

        public IUserRepository Users { get; private set; }
        public IBookRepository Books { get; private set; }
        public INoteRepository Notes { get; private set; }
        public IOrderRepository Orders { get; private set; }
        public IBookShareRepository BookShares { get; private set; }
        public ICategoryRepository Categories { get; private set; }
        public INoteShareRepository NoteShares { get; private set; }
        public IOrderDetailRepository OrderDetails { get; private set; }

        public UnitOfWork(LibraryArchiveContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            Books = new BookRepository(_context);
            Notes = new NoteRepository(_context);
            Orders = new OrderRepository(_context);
            BookShares = new BookShareRepository(_context);
            Categories = new CategoryRepository(_context);
            NoteShares = new NoteShareRepository(_context);
            OrderDetails = new OrderDetailRepository(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
