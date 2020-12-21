using FundooModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooRepositoryLayer
{
    public class LableRepository
    {
        private FundooDBContext _context;
        public LableRepository(FundooDBContext context) 
        {
            _context = context;
        }

        public Lable AddLable(int AccountId,int NoteId,Lable lable) 
        {
            lable.NoteId = NoteId;
            var result = _context.Lable.Add(lable);
            _context.SaveChanges();
            return result.Entity;
        }

        public int DeleteLable(int LableId) 
        {
           Lable lable= _context.Lable.Find(LableId);
           var result= _context.Lable.Remove(lable);
           return result.Entity.LableId;
        }
    }
}
