using FundooModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FundooRepositoryLayer
{
    public class RegistrationRepository: IRegistrationRepository
    {
        private FundooDBContext _context;
        public RegistrationRepository(FundooDBContext context)
        {
            _context = context;
        }

        public Account RegisterUser(Account account)
        {
            var result = _context.Account.Add(account);
            _context.SaveChanges();
            return result.Entity;
        }

        public Account LoginUser(Account account)
        {
            Account result = _context.Account.FirstOrDefault(Account=>Account.EmailId==account.EmailId && Account.Password==account.Password);
            return result;
        }
    }
}

