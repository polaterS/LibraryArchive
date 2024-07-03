using AutoMapper;
using LibraryArchive.Data.Entities;
using LibraryArchive.Services.DTOs.Note;
using LibraryArchive.Services.DTOs.Notification;
using LibraryArchive.Services.Repositories.Interfaces;
using LibraryArchive.Services.TaskManager.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryArchive.Services
{
    public class NoteService
    {
        private readonly INoteRepository _noteRepository;
        private readonly INotificationSenderService _notificationSenderService;
        private readonly IMapper _mapper;
        private readonly ILogger<NoteService> _logger;

        public NoteService(INoteRepository noteRepository, INotificationSenderService notificationSenderService, IMapper mapper, ILogger<NoteService> logger)
        {
            _noteRepository = noteRepository;
            _notificationSenderService = notificationSenderService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<NoteReadDto>> GetAllNotesAsync()
        {
            var notes = await _noteRepository.GetAllNotesAsync();
            return _mapper.Map<IEnumerable<NoteReadDto>>(notes);
        }

        public async Task<NoteReadDto> GetNoteByIdAsync(int noteId)
        {
            var note = await _noteRepository.GetNoteByIdAsync(noteId);
            return note != null ? _mapper.Map<NoteReadDto>(note) : null;
        }

        public async Task<NoteReadDto> AddNoteAsync(NoteCreateDto noteDto)
        {
            var note = _mapper.Map<Note>(noteDto);
            var addedNote = await _noteRepository.AddNoteAsync(note);

            // Not eklendiğinde bildirim gönderme
            var notificationDto = new NotificationCreateDto
            {
                UserId = note.UserId,
                Title = "New Note Added",
                Message = $"A new note '{note.Content}' has been added.",
                Date = DateTime.Now,
                NotificationType = "Email" // veya "SMS", "PushNotification"
            };

            await _notificationSenderService.SendNotificationAsync(notificationDto);

            return _mapper.Map<NoteReadDto>(addedNote);
        }

        public async Task<NoteReadDto> UpdateNoteAsync(NoteUpdateDto noteDto)
        {
            var note = await _noteRepository.GetNoteByIdAsync(noteDto.NoteId);
            if (note != null)
            {
                _mapper.Map(noteDto, note);
                var updatedNote = await _noteRepository.UpdateNoteAsync(note);

                // Not güncellendiğinde bildirim gönderme
                var notificationDto = new NotificationCreateDto
                {
                    UserId = note.UserId,
                    Title = "Note Updated",
                    Message = $"The note '{note.Content}' has been updated.",
                    Date = DateTime.Now,
                    NotificationType = "Email" // veya "SMS", "PushNotification"
                };

                await _notificationSenderService.SendNotificationAsync(notificationDto);

                return _mapper.Map<NoteReadDto>(updatedNote);
            }
            return null;
        }

        public async Task<bool> DeleteNoteAsync(int noteId)
        {
            return await _noteRepository.DeleteNoteAsync(noteId);
        }
    }
}
