using FundooModelLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FundooRepositoryLayer
{
    public class NotesRepository:INotesRepository
    {
        private FundooDBContext _context;
        public NotesRepository(FundooDBContext context) 
        {
            _context = context;
        }

        public List<Note> GetNotes(int AccountID) 
        {
            return _context.Note.OrderByDescending(Note => Note.IsPin).Where(Note => Note.AccountId == AccountID).ToList();
        }

        public Note AddNote(int AccountId, Note note) 
        {
            note.AccountId = AccountId;
            var result = _context.Note.Add(note);
            _context.SaveChanges();
            return result.Entity;
        }

        public Note GetNote(int AccountId,int  NoteId) 
        {
            return _context.Note.FirstOrDefault(Note => Note.NoteId == NoteId && Note.AccountId == AccountId);
        }

        public int DeleteNote(int AccountId,int NoteId)
        {
            Note note = _context.Note.FirstOrDefault(Note => Note.NoteId == NoteId && Note.AccountId == AccountId);
            _context.Note.Remove(note);
            _context.SaveChanges();
            return NoteId;
        }

        public Note UpdateNote(int AccountId, int NoteId,Note NewNote) 
        { 
            Note note = _context.Note.FirstOrDefault(Note => Note.NoteId == NoteId && Note.AccountId == AccountId);
            note.Title = NewNote.Title;
            note.Image = NewNote.Image;
            note.IsPin = NewNote.IsPin;
            note.Remainder = NewNote.Remainder;
            note.Description = NewNote.Description;
            note.Color = NewNote.Color;
            _context.SaveChanges();
            return note;
        }
    }
}
