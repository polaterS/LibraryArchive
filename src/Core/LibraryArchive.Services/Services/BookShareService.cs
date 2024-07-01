using LibraryArchive.Data.Entities;
using LibraryArchive.Services.DTOs.BookShare;
using LibraryArchive.Services.Repositories.Interfaces;

namespace LibraryArchive.Services
{
    public class BookShareService
    {
        private readonly IBookShareRepository _bookShareRepository;

        public BookShareService(IBookShareRepository bookShareRepository)
        {
            _bookShareRepository = bookShareRepository;
        }

        public async Task<IEnumerable<BookShareReadDto>> GetAllBookSharesAsync()
        {
            var bookShares = await _bookShareRepository.GetAllBookSharesAsync();
            var bookShareDtos = new List<BookShareReadDto>();

            foreach (var bookShare in bookShares)
            {
                bookShareDtos.Add(new BookShareReadDto
                {
                    BookShareId = bookShare.BookShareId,
                    NoteId = bookShare.NoteId,
                    SharedWithUserId = bookShare.SharedWithUserId,
                    ShareType = bookShare.ShareType
                });
            }

            return bookShareDtos;
        }

        public async Task<BookShareReadDto> GetBookShareByIdAsync(int bookShareId)
        {
            var bookShare = await _bookShareRepository.GetBookShareByIdAsync(bookShareId);
            if (bookShare != null)
            {
                return new BookShareReadDto
                {
                    BookShareId = bookShare.BookShareId,
                    NoteId = bookShare.NoteId,
                    SharedWithUserId = bookShare.SharedWithUserId,
                    ShareType = bookShare.ShareType
                };
            }
            return null;
        }

        public async Task<BookShare> AddBookShareAsync(BookShareCreateDto bookShareDto)
        {
            var bookShare = new BookShare
            {
                NoteId = bookShareDto.NoteId,
                SharedWithUserId = bookShareDto.SharedWithUserId,
                ShareType = bookShareDto.ShareType
            };

            return await _bookShareRepository.AddBookShareAsync(bookShare);
        }

        public async Task<BookShare> UpdateBookShareAsync(BookShareUpdateDto bookShareDto)
        {
            var bookShare = await _bookShareRepository.GetBookShareByIdAsync(bookShareDto.BookShareId);
            if (bookShare != null)
            {
                bookShare.NoteId = bookShareDto.NoteId;
                bookShare.SharedWithUserId = bookShareDto.SharedWithUserId;
                bookShare.ShareType = bookShareDto.ShareType;

                return await _bookShareRepository.UpdateBookShareAsync(bookShare);
            }
            return null;
        }

        public async Task<bool> DeleteBookShareAsync(int bookShareId)
        {
            var bookShare = await _bookShareRepository.DeleteBookShareAsync(bookShareId);
            return bookShare != null;
        }
    }
}
