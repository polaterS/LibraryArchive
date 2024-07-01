using AutoMapper;
using LibraryArchive.Data.Entities;
using LibraryArchive.Services.DTOs.BookShare;
using LibraryArchive.Services.Repositories.Interfaces;

namespace LibraryArchive.Services
{
    public class BookShareService
    {
        private readonly IBookShareRepository _bookShareRepository;
        private readonly IMapper _mapper;

        public BookShareService(IBookShareRepository bookShareRepository, IMapper mapper)
        {
            _bookShareRepository = bookShareRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookShareReadDto>> GetAllBookSharesAsync()
        {
            var bookShares = await _bookShareRepository.GetAllBookSharesAsync();
            return _mapper.Map<IEnumerable<BookShareReadDto>>(bookShares);
        }

        public async Task<BookShareReadDto> GetBookShareByIdAsync(int bookShareId)
        {
            var bookShare = await _bookShareRepository.GetBookShareByIdAsync(bookShareId);
            return bookShare != null ? _mapper.Map<BookShareReadDto>(bookShare) : null;
        }

        public async Task<BookShareReadDto> AddBookShareAsync(BookShareCreateDto bookShareDto)
        {
            var bookShare = _mapper.Map<BookShare>(bookShareDto);
            var addedBookShare = await _bookShareRepository.AddBookShareAsync(bookShare);
            return _mapper.Map<BookShareReadDto>(addedBookShare);
        }

        public async Task<BookShareReadDto> UpdateBookShareAsync(BookShareUpdateDto bookShareDto)
        {
            var bookShare = await _bookShareRepository.GetBookShareByIdAsync(bookShareDto.BookShareId);
            if (bookShare != null)
            {
                _mapper.Map(bookShareDto, bookShare);
                var updatedBookShare = await _bookShareRepository.UpdateBookShareAsync(bookShare);
                return _mapper.Map<BookShareReadDto>(updatedBookShare);
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
