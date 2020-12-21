using FundooModelLayer;
using FundooRepositoryLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooServiceLayer
{
    public class NoteService:INoteService
    {
        private INotesRepository _notesRepository;
        public NoteService( INotesRepository notesRepository) 
        {
            _notesRepository = notesRepository;
        }

        public List<Note> GetNotes(int AccountId)
        {
            return _notesRepository.GetNotes( AccountId);
        }

        public Note AddNote(int AccountId, Note note) 
        {
            return _notesRepository.AddNote(AccountId, note);
        }

        public Note GetNote(int AccountId, int NoteId) 
        {
            return _notesRepository.GetNote(AccountId, NoteId);
        }
        public int  DeleteNote(int AccountId, int NoteId) 
        {
            return _notesRepository.DeleteNote(AccountId, NoteId);
        }

        public Note UpdateNote(int AccountId, int NoteId,Note note)
        {
            return _notesRepository.UpdateNote( AccountId, NoteId,note);
        }
    }
}
