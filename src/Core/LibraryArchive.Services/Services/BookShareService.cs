using AutoMapper;
using FluentValidation;
using LibraryArchive.Data.Entities;
using LibraryArchive.Services.DTOs.BookShare;
using LibraryArchive.Services.Repositories.Interfaces;
using Serilog;

namespace LibraryArchive.Services
{
    public class BookShareService
    {
        private readonly IBookShareRepository _bookShareRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IValidator<BookShareCreateDto> _bookShareCreateValidator;
        private readonly IValidator<BookShareUpdateDto> _bookShareUpdateValidator;
        private readonly IValidator<BookShareDeleteDto> _bookShareDeleteValidator;

        public BookShareService(
            IBookShareRepository bookShareRepository,
            IMapper mapper,
            IValidator<BookShareCreateDto> bookShareCreateValidator,
            IValidator<BookShareUpdateDto> bookShareUpdateValidator,
            IValidator<BookShareDeleteDto> bookShareDeleteValidator)
        {
            _bookShareRepository = bookShareRepository;
            _mapper = mapper;
            _logger = Log.ForContext<BookShareService>();
            _bookShareCreateValidator = bookShareCreateValidator;
            _bookShareUpdateValidator = bookShareUpdateValidator;
            _bookShareDeleteValidator = bookShareDeleteValidator;
        }

        public async Task<BookShareReadDto> GetBookShareByIdAsync(int bookShareId)
        {
            try
            {
                _logger.Information("Getting book share by ID: {BookShareId}", bookShareId);
                var bookShare = await _bookShareRepository.GetBookShareByIdAsync(bookShareId);
                return _mapper.Map<BookShareReadDto>(bookShare);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting book share by ID: {BookShareId}", bookShareId);
                throw;
            }
        }

        public async Task<IEnumerable<BookShareReadDto>> GetAllBookSharesAsync()
        {
            try
            {
                _logger.Information("Getting all book shares");
                var bookShares = await _bookShareRepository.GetAllBookSharesAsync();
                return _mapper.Map<IEnumerable<BookShareReadDto>>(bookShares);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting all book shares");
                throw;
            }
        }

        public async Task<IEnumerable<BookShareReadDto>> GetBookSharesByUserIdAsync(string userId)
        {
            try
            {
                _logger.Information("Getting book shares by user ID: {UserId}", userId);
                var bookShares = await _bookShareRepository.GetBookSharesByUserIdAsync(userId);
                return _mapper.Map<IEnumerable<BookShareReadDto>>(bookShares);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting book shares by user ID: {UserId}", userId);
                throw;
            }
        }

        public async Task AddBookShareAsync(BookShareCreateDto bookShareCreateDto)
        {
            await _bookShareCreateValidator.ValidateAndThrowAsync(bookShareCreateDto);
            try
            {
                var bookShare = _mapper.Map<BookShare>(bookShareCreateDto);
                _logger.Information("Adding book share: {BookShare}", bookShare);
                await _bookShareRepository.AddBookShareAsync(bookShare);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error adding book share: {BookShareCreateDto}", bookShareCreateDto);
                throw;
            }
        }

        public void RemoveBookShare(BookShareDeleteDto bookShareDeleteDto)
        {
            _bookShareDeleteValidator.ValidateAndThrow(bookShareDeleteDto);
            try
            {
                var bookShare = _mapper.Map<BookShare>(bookShareDeleteDto);
                _logger.Information("Removing book share: {BookShare}", bookShare);
                _bookShareRepository.RemoveBookShare(bookShare);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error removing book share: {BookShareDeleteDto}", bookShareDeleteDto);
                throw;
            }
        }

        public void UpdateBookShare(BookShareUpdateDto bookShareUpdateDto)
        {
            _bookShareUpdateValidator.ValidateAndThrow(bookShareUpdateDto);
            try
            {
                var bookShare = _mapper.Map<BookShare>(bookShareUpdateDto);
                _logger.Information("Updating book share: {BookShare}", bookShare);
                _bookShareRepository.UpdateBookShare(bookShare);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error updating book share: {BookShareUpdateDto}", bookShareUpdateDto);
                throw;
            }
        }
    }
}
