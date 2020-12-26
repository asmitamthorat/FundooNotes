using FundooModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooRepositoryLayer
{
    public class LableRepository:ILableRepository
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
           _context.SaveChanges();
           return result.Entity.LableId;
        }

        public Lable GetLabel(int LabelId) 
        {
            Lable label = _context.Lable.Find(LabelId);
            return label;
        }
    }
}
