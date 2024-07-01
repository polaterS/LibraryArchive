using LibraryArchive.Data.Entities;
using LibraryArchive.Services.DTOs.NoteShare;
using LibraryArchive.Services.Repositories.Interfaces;

namespace LibraryArchive.Services
{
    public class NoteShareService
    {
        private readonly INoteShareRepository _noteShareRepository;

        public NoteShareService(INoteShareRepository noteShareRepository)
        {
            _noteShareRepository = noteShareRepository;
        }

        public async Task<IEnumerable<NoteShareReadDto>> GetAllNoteSharesAsync()
        {
            var noteShares = await _noteShareRepository.GetAllNoteSharesAsync();
            return noteShares.Select(ns => new NoteShareReadDto
            {
                NoteShareId = ns.NoteShareId,
                NoteId = ns.NoteId,
                SharedWithUserId = ns.SharedWithUserId,
                ShareType = ns.ShareType
            }).ToList();
        }

        public async Task<NoteShareReadDto> GetNoteShareByIdAsync(int noteShareId)
        {
            var noteShare = await _noteShareRepository.GetNoteShareByIdAsync(noteShareId);
            if (noteShare != null)
            {
                return new NoteShareReadDto
                {
                    NoteShareId = noteShare.NoteShareId,
                    NoteId = noteShare.NoteId,
                    SharedWithUserId = noteShare.SharedWithUserId,
                    ShareType = noteShare.ShareType
                };
            }
            return null;
        }

        public async Task<NoteShare> AddNoteShareAsync(NoteShareCreateDto noteShareDto)
        {
            var noteShare = new NoteShare
            {
                NoteId = noteShareDto.NoteId,
                SharedWithUserId = noteShareDto.SharedWithUserId,
                ShareType = noteShareDto.ShareType
            };

            return await _noteShareRepository.AddNoteShareAsync(noteShare);
        }

        public async Task<NoteShare> UpdateNoteShareAsync(NoteShareUpdateDto noteShareDto)
        {
            var noteShare = await _noteShareRepository.GetNoteShareByIdAsync(noteShareDto.NoteShareId);
            if (noteShare != null)
            {
                noteShare.NoteId = noteShareDto.NoteId;
                noteShare.SharedWithUserId = noteShareDto.SharedWithUserId;
                noteShare.ShareType = noteShareDto.ShareType;

                return await _noteShareRepository.UpdateNoteShareAsync(noteShare);
            }
            return null;
        }

        public async Task<bool> DeleteNoteShareAsync(int noteShareId)
        {
            return await _noteShareRepository.DeleteNoteShareAsync(noteShareId);
        }
    }
}
