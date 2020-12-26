using FundooModelLayer;
using FundooServiceLayer;
using FundooServiceLayer.MSMQService;
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
        IMSMQ _mamq;
      //  MSMQ mSMQ = new MSMQ();
        public AccountController(IRegistrationService registrationService,IMSMQ msmq) 
        {
            _registrationService = registrationService;
            _mamq = msmq;
        }


        [HttpPost]
        [Route("RegisterUser")]
        public ActionResult RegisterUser([FromBody]Account account)
        {
            Account user = _registrationService.RegisterUser(account);
            try
            {
                if (user == null)
                {
                    return NotFound(new ServiceResponse<Account> { StatusCode = (int)HttpStatusCode.NotFound, Message = "Internal Server Error", Data = null });
                }
                _mamq.AddToQueue(account.EmailId + " " + "Registered Successfully " + "  " + System.DateTime.Now.ToString());
                return Ok(new ServiceResponse<Account> { StatusCode = (int)HttpStatusCode.OK, Message = "successful", Data = user });
            } 
            catch (Exception)
            {
                return BadRequest(new ServiceResponse<Account> { StatusCode = (int)HttpStatusCode.BadRequest, Message = "Page Not Found", Data = null });
            }
           
        }

        [HttpPost]
        [Route("LoginUser")]
        public ActionResult LoginUser([FromBody] Account account)  
        {
            try
            {
                string user = _registrationService.LoginUser(account);
                if (user == null)
                {
                    return NotFound(new ServiceResponse<Account> { StatusCode = (int)HttpStatusCode.NotFound, Message = "Not Found", Data = null });
                }
                _mamq.AddToQueue(account.EmailId + " " + " login successful" +"  "+ System.DateTime.Now.ToString());
                return Ok(new ServiceResponse<string> { StatusCode = (int)HttpStatusCode.OK, Message = "successful", Data = user });
            }
            catch (Exception) 
            {
                return BadRequest(new ServiceResponse<Account> { StatusCode = (int)HttpStatusCode.BadRequest, Message = "Page Not Found", Data = null });
            }
        }
    }
}
