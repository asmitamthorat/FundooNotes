using FundooModelLayer;
using FundooRepositoryLayer;
using FundooServiceLayer.CoundForImages;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooServiceLayer
{
    public class NoteService:INoteService
    {
        private INotesRepository _notesRepository;
        private ICloudForImages _cloudForImages;
        public NoteService( INotesRepository notesRepository,ICloudForImages cloudForImages) 
        {
            _notesRepository = notesRepository;
            _cloudForImages = cloudForImages;
        }

        public List<NotesViewModel> GetNotes(int AccountId)
        {
          //  List<Note> notes= GetNotes(AccountId);
            
            return _notesRepository.GetNotes( AccountId);
        }

        public Note AddNote(int AccountId, NoteForCloud noteFromCloud, string email) 
        {
            string url = _cloudForImages.UploadToCloud(noteFromCloud.Image,email);
            Note note = new Note {Title=noteFromCloud.Title,Description=noteFromCloud.Description,IsPin=noteFromCloud.IsPin,Remainder=noteFromCloud.Remainder,
                                  Image=url};
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
