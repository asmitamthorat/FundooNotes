using FundooModelLayer;
using FundooRepositoryLayer;
using FundooServiceLayer.TokenAuthentification;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooServiceLayer
{
    public class RegistrationService:IRegistrationService
    {
        private IRegistrationRepository _registrationRepository;
        private ITokenManager _tokenManager;
        public RegistrationService(IRegistrationRepository registrationRepository, ITokenManager tokenManager)
        {
            _registrationRepository = registrationRepository;
            _tokenManager = tokenManager;
        }

        public Account RegisterUser(Account account) 
        {
            return _registrationRepository.RegisterUser(account);
        }

        public string LoginUser(Account account) 
        {
           Account user= _registrationRepository.LoginUser(account);
           var tokenString = _tokenManager.GenerateToken(user);
           return tokenString;
        }
    }
}
