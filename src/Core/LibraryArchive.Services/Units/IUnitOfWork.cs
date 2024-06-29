﻿using LibraryArchive.Services.Repositories.Interfaces;

namespace LibraryArchive.Services.Units
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IBookRepository Books { get; }
        INoteRepository Notes { get; }
        IOrderRepository Orders { get; }
        IBookShareRepository BookShares { get; }
        ICategoryRepository Categories { get; }
        INoteShareRepository NoteShares { get; }
        IOrderDetailRepository OrderDetails { get; }

        Task<int> CompleteAsync();  // Asenkron değişiklikleri kaydetmek için
        int Complete();  // Senkron değişiklikleri kaydetmek için
    }
}
