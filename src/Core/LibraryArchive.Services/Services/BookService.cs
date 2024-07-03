using AutoMapper;
using LibraryArchive.Data.Entities;
using LibraryArchive.Services.DTOs.Book;
using LibraryArchive.Services.DTOs.Notification;
using LibraryArchive.Services.Repositories.Interfaces;
using LibraryArchive.Services.TaskManager.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryArchive.Services
{
    public class BookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly INotificationSenderService _notificationSenderService;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<BookService> _logger;

        public BookService(IBookRepository bookRepository, INotificationSenderService notificationSenderService, IMapper mapper, UserManager<ApplicationUser> userManager, ILogger<BookService> logger)
        {
            _bookRepository = bookRepository;
            _notificationSenderService = notificationSenderService;
            _mapper = mapper;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IEnumerable<BookReadDto>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllBooksAsync();
            return _mapper.Map<IEnumerable<BookReadDto>>(books);
        }

        public async Task<BookReadDto> GetBookByIdAsync(int bookId)
        {
            var book = await _bookRepository.GetBookByIdAsync(bookId);
            return book != null ? _mapper.Map<BookReadDto>(book) : null;
        }

        public async Task<BookReadDto> AddBookAsync(BookCreateDto bookDto, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("Invalid user ID");
            }

            var book = _mapper.Map<Book>(bookDto);
            book.UserId = userId;

            await _bookRepository.AddBookAsync(book);

            // Kitap eklendiğinde bildirim gönderme
            var notificationDto = new NotificationCreateDto
            {
                UserId = userId,
                Title = "New Book Added",
                Message = $"A new book '{book.Title}' has been added.",
                Date = DateTime.Now,
                NotificationType = "Email" // veya "SMS", "PushNotification"
            };

            await _notificationSenderService.SendNotificationAsync(notificationDto);

            return _mapper.Map<BookReadDto>(book);
        }

        public async Task<BookReadDto> UpdateBookAsync(BookUpdateDto bookDto)
        {
            var book = await _bookRepository.GetBookByIdAsync(bookDto.BookId);
            if (book == null)
            {
                return null; 
            }
            _mapper.Map(bookDto, book);
            await _bookRepository.UpdateBookAsync(book);

            // Kitap güncellendiğinde bildirim gönderme
            var notificationDto = new NotificationCreateDto
            {
                UserId = book.UserId,
                Title = "Book Updated",
                Message = $"The book '{book.Title}' has been updated.",
                Date = DateTime.Now,
                NotificationType = "Email" // veya "SMS", "PushNotification"
            };

            await _notificationSenderService.SendNotificationAsync(notificationDto);

            return _mapper.Map<BookReadDto>(book);
        }

        public async Task<bool> DeleteBookAsync(int bookId)
        {
            var book = await _bookRepository.DeleteBookAsync(bookId);
            return book != null;
        }

        public async Task<IEnumerable<BookReadDto>> SearchBooksAsync(string searchTerm)
        {
            var books = await _bookRepository.SearchBooksAsync(searchTerm);
            return _mapper.Map<IEnumerable<BookReadDto>>(books);
        }
    }
}
