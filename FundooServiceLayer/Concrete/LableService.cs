using FundooModelLayer;
using FundooRepositoryLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooServiceLayer
{
    public class LableService:ILableService
    {
        ILableRepository _LableRepository;
        public LableService(ILableRepository LableRepository) 
        {
            _LableRepository = LableRepository;
        }

        public Lable AddLable(int AccountId, int NoteId,Lable lable) 
        {
            return _LableRepository.AddLable( AccountId, NoteId, lable);
        }

        public int DeleteLable(int AccountId, int LableId) 
        {
            return _LableRepository.DeleteLable(LableId);
        }

        public Lable GetLabel(int LabelId) 
        {
            return _LableRepository.GetLabel(LabelId);
        }
    }
}
