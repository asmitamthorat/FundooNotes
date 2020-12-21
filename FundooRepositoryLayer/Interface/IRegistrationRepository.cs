using FundooModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooRepositoryLayer
{
    public interface IRegistrationRepository
    {
        Account RegisterUser(Account account);

        Account LoginUser(Account account);
    }
}
