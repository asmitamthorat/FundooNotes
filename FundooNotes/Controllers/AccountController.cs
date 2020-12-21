using FundooModelLayer;
using FundooServiceLayer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    public class AccountController:ControllerBase
    {
        IRegistrationService _registrationService; 
        public AccountController(IRegistrationService registrationService) 
        {
            _registrationService = registrationService;
        }


        [HttpPost]
        [Route("RegisterUser")]
        public ActionResult RegisterUser([FromBody]Account account)
        {
            Account user = _registrationService.RegisterUser(account);
            if (user == null)
            {
                return NotFound(new ServiceResponse<List<Note>> { StatusCode = (int)HttpStatusCode.NotFound, Message = "Not Found", Data = null });
            }
            return Ok(new ServiceResponse<Account> { StatusCode = (int)HttpStatusCode.OK, Message = "successful", Data = user });
        }

        [HttpPost]
        [Route("LoginUser")]
        public ActionResult LoginUser([FromBody] Account account)  
        {
            string user = _registrationService.LoginUser(account);
            if (user == null)
            {
                return NotFound(new ServiceResponse<Account> { StatusCode = (int)HttpStatusCode.NotFound, Message = "Not Found", Data = null });
            }
            return Ok(new ServiceResponse<string> { StatusCode = (int)HttpStatusCode.OK, Message = "successful", Data = user });
        }
    }
}
