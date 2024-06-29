using AutoMapper;
using FluentValidation;
using LibraryArchive.Data.Entities;
using LibraryArchive.Services.DTOs.NoteShare;
using LibraryArchive.Services.Repositories.Interfaces;
using Serilog;

namespace LibraryArchive.Services
{
    public class NoteShareService
    {
        private readonly INoteShareRepository _noteShareRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IValidator<NoteShareCreateDto> _noteShareCreateValidator;
        private readonly IValidator<NoteShareUpdateDto> _noteShareUpdateValidator;
        private readonly IValidator<NoteShareDeleteDto> _noteShareDeleteValidator;

        public NoteShareService(
            INoteShareRepository noteShareRepository,
            IMapper mapper,
            IValidator<NoteShareCreateDto> noteShareCreateValidator,
            IValidator<NoteShareUpdateDto> noteShareUpdateValidator,
            IValidator<NoteShareDeleteDto> noteShareDeleteValidator)
        {
            _noteShareRepository = noteShareRepository;
            _mapper = mapper;
            _logger = Log.ForContext<NoteShareService>();
            _noteShareCreateValidator = noteShareCreateValidator;
            _noteShareUpdateValidator = noteShareUpdateValidator;
            _noteShareDeleteValidator = noteShareDeleteValidator;
        }

        public async Task<NoteShareReadDto> GetNoteShareByIdAsync(int noteShareId)
        {
            try
            {
                _logger.Information("Getting note share by ID: {NoteShareId}", noteShareId);
                var noteShare = await _noteShareRepository.GetNoteShareByIdAsync(noteShareId);
                return _mapper.Map<NoteShareReadDto>(noteShare);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting note share by ID: {NoteShareId}", noteShareId);
                throw;
            }
        }

        public async Task<IEnumerable<NoteShareReadDto>> GetAllNoteSharesAsync()
        {
            try
            {
                _logger.Information("Getting all note shares");
                var noteShares = await _noteShareRepository.GetAllNoteSharesAsync();
                return _mapper.Map<IEnumerable<NoteShareReadDto>>(noteShares);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting all note shares");
                throw;
            }
        }

        public async Task<IEnumerable<NoteShareReadDto>> GetNoteSharesByUserIdAsync(string userId)
        {
            try
            {
                _logger.Information("Getting note shares by user ID: {UserId}", userId);
                var noteShares = await _noteShareRepository.GetNoteSharesByUserIdAsync(userId);
                return _mapper.Map<IEnumerable<NoteShareReadDto>>(noteShares);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting note shares by user ID: {UserId}", userId);
                throw;
            }
        }

        public async Task AddNoteShareAsync(NoteShareCreateDto noteShareCreateDto)
        {
            await _noteShareCreateValidator.ValidateAndThrowAsync(noteShareCreateDto);
            try
            {
                var noteShare = _mapper.Map<NoteShare>(noteShareCreateDto);
                _logger.Information("Adding note share: {NoteShare}", noteShare);
                await _noteShareRepository.AddNoteShareAsync(noteShare);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error adding note share: {NoteShareCreateDto}", noteShareCreateDto);
                throw;
            }
        }

        public void RemoveNoteShare(NoteShareDeleteDto noteShareDeleteDto)
        {
            _noteShareDeleteValidator.ValidateAndThrow(noteShareDeleteDto);
            try
            {
                var noteShare = _mapper.Map<NoteShare>(noteShareDeleteDto);
                _logger.Information("Removing note share: {NoteShare}", noteShare);
                _noteShareRepository.RemoveNoteShare(noteShare);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error removing note share: {NoteShareDeleteDto}", noteShareDeleteDto);
                throw;
            }
        }

        public void UpdateNoteShare(NoteShareUpdateDto noteShareUpdateDto)
        {
            _noteShareUpdateValidator.ValidateAndThrow(noteShareUpdateDto);
            try
            {
                var noteShare = _mapper.Map<NoteShare>(noteShareUpdateDto);
                _logger.Information("Updating note share: {NoteShare}", noteShare);
                _noteShareRepository.UpdateNoteShare(noteShare);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error updating note share: {NoteShareUpdateDto}", noteShareUpdateDto);
                throw;
            }
        }
    }
}
