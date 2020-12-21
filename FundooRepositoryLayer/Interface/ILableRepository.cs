using FundooModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooRepositoryLayer
{
    public interface ILableRepository
    {

      Lable AddLable(int AccountId, int NoteId, Lable lable);

        int DeleteLable(int LableId);
    }
}
