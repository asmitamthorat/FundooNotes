using FundooModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooServiceLayer
{
    public interface INoteService
    {
        List<NotesViewModel> GetNotes(int AccountId);
        Note AddNote(int userId, NoteForCloud note,string email);

        Note GetNote(int AccountId, int NoteId);

        int DeleteNote(int AccountId, int NoteId);

        Note UpdateNote(int AccountId, int NoteId, Note note);
    }
}
