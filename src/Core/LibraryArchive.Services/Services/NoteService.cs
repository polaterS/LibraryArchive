using AutoMapper;
using FluentValidation;
using LibraryArchive.Data.Entities;
using LibraryArchive.Services.DTOs.Note;
using LibraryArchive.Services.Repositories.Interfaces;
using Serilog;

namespace LibraryArchive.Services
{
    public class NoteService
    {
        private readonly INoteRepository _noteRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IValidator<NoteCreateDto> _noteCreateValidator;
        private readonly IValidator<NoteUpdateDto> _noteUpdateValidator;
        private readonly IValidator<NoteDeleteDto> _noteDeleteValidator;

        public NoteService(
            INoteRepository noteRepository,
            IMapper mapper,
            IValidator<NoteCreateDto> noteCreateValidator,
            IValidator<NoteUpdateDto> noteUpdateValidator,
            IValidator<NoteDeleteDto> noteDeleteValidator)
        {
            _noteRepository = noteRepository;
            _mapper = mapper;
            _logger = Log.ForContext<NoteService>();
            _noteCreateValidator = noteCreateValidator;
            _noteUpdateValidator = noteUpdateValidator;
            _noteDeleteValidator = noteDeleteValidator;
        }

        public async Task<NoteReadDto> GetNoteByIdAsync(int noteId)
        {
            try
            {
                _logger.Information("Getting note by ID: {NoteId}", noteId);
                var note = await _noteRepository.GetNoteByIdAsync(noteId);
                return _mapper.Map<NoteReadDto>(note);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting note by ID: {NoteId}", noteId);
                throw;
            }
        }

        public async Task<IEnumerable<NoteReadDto>> GetAllNotesAsync()
        {
            try
            {
                _logger.Information("Getting all notes");
                var notes = await _noteRepository.GetAllNotesAsync();
                return _mapper.Map<IEnumerable<NoteReadDto>>(notes);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting all notes");
                throw;
            }
        }

        public async Task<IEnumerable<NoteReadDto>> GetNotesByUserIdAsync(string userId)
        {
            try
            {
                _logger.Information("Getting notes by user ID: {UserId}", userId);
                var notes = await _noteRepository.GetNotesByUserIdAsync(userId);
                return _mapper.Map<IEnumerable<NoteReadDto>>(notes);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting notes by user ID: {UserId}", userId);
                throw;
            }
        }

        public async Task AddNoteAsync(NoteCreateDto noteCreateDto)
        {
            await _noteCreateValidator.ValidateAndThrowAsync(noteCreateDto);
            try
            {
                var note = _mapper.Map<Note>(noteCreateDto);
                _logger.Information("Adding note: {Note}", note);
                await _noteRepository.AddNoteAsync(note);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error adding note: {NoteCreateDto}", noteCreateDto);
                throw;
            }
        }

        public void RemoveNote(NoteDeleteDto noteDeleteDto)
        {
            _noteDeleteValidator.ValidateAndThrow(noteDeleteDto);
            try
            {
                var note = _mapper.Map<Note>(noteDeleteDto);
                _logger.Information("Removing note: {Note}", note);
                _noteRepository.RemoveNote(note);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error removing note: {NoteDeleteDto}", noteDeleteDto);
                throw;
            }
        }

        public void UpdateNote(NoteUpdateDto noteUpdateDto)
        {
            _noteUpdateValidator.ValidateAndThrow(noteUpdateDto);
            try
            {
                var note = _mapper.Map<Note>(noteUpdateDto);
                _logger.Information("Updating note: {Note}", note);
                _noteRepository.UpdateNote(note);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error updating note: {NoteUpdateDto}", noteUpdateDto);
                throw;
            }
        }
    }
}
