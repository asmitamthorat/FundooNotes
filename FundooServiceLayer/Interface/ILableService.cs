using FundooModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooServiceLayer
{
    public interface ILableService
    {
        Lable AddLable(int AccountId, int LableId, Lable lable);
        int DeleteLable(int AccountId, int LableId);
    }
}
