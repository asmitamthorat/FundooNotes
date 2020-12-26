using FundooModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooRepositoryLayer
{
    public interface INotesRepository
    {
        List<NotesViewModel> GetNotes(int AccountID);
        Note AddNote(int AccountId, Note note);

        Note GetNote(int AccountId, int NoteId);

        int DeleteNote(int AccountId, int NoteId);

        Note UpdateNote(int AccountId, int NoteId, Note NewNote);

    }
}
