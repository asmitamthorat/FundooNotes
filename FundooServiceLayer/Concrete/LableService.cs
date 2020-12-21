using FundooModelLayer;
using FundooRepositoryLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooServiceLayer
{
    public class LableService
    {
        ILableRepository _LableRepository;
        public LableService(ILableRepository LableRepository) 
        {
            _LableRepository = LableRepository;
        }

        public Lable AddLable(int AccountId, int LableId,Lable lable) 
        {
            return _LableRepository.AddLable( AccountId, LableId, lable);
        }

        public int DeleteLable(int AccountId, int LableId) 
        {
            return _LableRepository.DeleteLable(LableId);
        }
    }
}
