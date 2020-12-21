using FundooModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooServiceLayer
{
    public interface IRegistrationService
    {
        Account RegisterUser(Account account);

        string LoginUser(Account account);
    }
}
