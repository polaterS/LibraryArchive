using AutoMapper;
using LibraryArchive.Data.Entities;
using LibraryArchive.Services.DTOs.NoteShare;
using LibraryArchive.Services.Repositories.Interfaces;

namespace LibraryArchive.Services
{
    public class NoteShareService
    {
        private readonly INoteShareRepository _noteShareRepository;
        private readonly IMapper _mapper;

        public NoteShareService(INoteShareRepository noteShareRepository, IMapper mapper)
        {
            _noteShareRepository = noteShareRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<NoteShareReadDto>> GetAllNoteSharesAsync()
        {
            var noteShares = await _noteShareRepository.GetAllNoteSharesAsync();
            return _mapper.Map<IEnumerable<NoteShareReadDto>>(noteShares);
        }

        public async Task<NoteShareReadDto> GetNoteShareByIdAsync(int noteShareId)
        {
            var noteShare = await _noteShareRepository.GetNoteShareByIdAsync(noteShareId);
            return noteShare != null ? _mapper.Map<NoteShareReadDto>(noteShare) : null;
        }

        public async Task<NoteShareReadDto> AddNoteShareAsync(NoteShareCreateDto noteShareDto)
        {
            var noteShare = _mapper.Map<NoteShare>(noteShareDto);
            var addedNoteShare = await _noteShareRepository.AddNoteShareAsync(noteShare);
            return _mapper.Map<NoteShareReadDto>(addedNoteShare);
        }

        public async Task<NoteShareReadDto> UpdateNoteShareAsync(NoteShareUpdateDto noteShareDto)
        {
            var noteShare = await _noteShareRepository.GetNoteShareByIdAsync(noteShareDto.NoteShareId);
            if (noteShare != null)
            {
                _mapper.Map(noteShareDto, noteShare);
                var updatedNoteShare = await _noteShareRepository.UpdateNoteShareAsync(noteShare);
                return _mapper.Map<NoteShareReadDto>(updatedNoteShare);
            }
            return null;
        }

        public async Task<bool> DeleteNoteShareAsync(int noteShareId)
        {
            return await _noteShareRepository.DeleteNoteShareAsync(noteShareId);
        }
    }
}
