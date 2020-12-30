using FundooModelLayer;
using FundooServiceLayer;
using FundooServiceLayer.MSMQService;
using FundooServiceLayer.TokenAuthentification;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LableController:ControllerBase
    {
        private ILableService _lableService;
        private IMSMQ _msmq;
        public LableController(ILableService lableService,IMSMQ msmq)
        {
            _lableService = lableService;
            _msmq = msmq;
        }

        [HttpPost]
        [TokenAuthenticationFilter]
        public ActionResult AddLable(int NoteId,[FromBody] Lable lable)
        {
            try
            {
                var AccountId = Convert.ToInt32(HttpContext.Items["userId"]);
                Lable result = _lableService.AddLable(AccountId, NoteId, lable);
                if (result == null)
                {
                    return NotFound(new ServiceResponse<Lable> { StatusCode = (int)HttpStatusCode.NotFound, Message = "Internal Server Error", Data = null });
                }
                _msmq.AddToQueue(AccountId + " " + "label Added Successfully " + "  " + System.DateTime.Now.ToString());
                return Ok(new ServiceResponse<Lable> { StatusCode = (int)HttpStatusCode.OK, Message = "successful", Data = result });
            }
            catch (Exception)
            {
                return BadRequest(new ServiceResponse<Lable> { StatusCode = (int)HttpStatusCode.BadRequest, Message = "Page Not Found", Data = null });
            } 
        }

        [HttpDelete]
        [TokenAuthenticationFilter]
        public ActionResult DeleteLable(int LableId)
        {
            try 
            {
                var AccountId = Convert.ToInt32(HttpContext.Items["userId"]);
                int result = _lableService.DeleteLable(AccountId, LableId);
                _msmq.AddToQueue(AccountId + " " + "label Deleted Successfully " + "  " + System.DateTime.Now.ToString());
                return Ok(new ServiceResponse<int> { StatusCode = (int)HttpStatusCode.OK, Message = "successful", Data = result });
            }
            catch (Exception)
            {
                return BadRequest(new ServiceResponse<Lable> { StatusCode = (int)HttpStatusCode.BadRequest, Message = "Page Not Found", Data = null });
            }
        }

        [HttpGet]
        [TokenAuthenticationFilter]
        public ActionResult GetLable(int LableId) 
        {
            try 
            {
                var AccountId = Convert.ToInt32(HttpContext.Items["userId"]);
                Lable label = _lableService.GetLabel(LableId);
                if (label == null)
                {
                    return NotFound(new ServiceResponse<Lable> { StatusCode = (int)HttpStatusCode.NotFound, Message = "Internal Server Error", Data = null });
                }
                _msmq.AddToQueue(AccountId + " " + "label seen " + "  " + System.DateTime.Now.ToString());
                return Ok(new ServiceResponse<Lable> { StatusCode = (int)HttpStatusCode.OK, Message = "successful", Data = label });
            } 
            catch (Exception)
            {
                return BadRequest(new ServiceResponse<Lable> { StatusCode = (int)HttpStatusCode.BadRequest, Message = "Page Not Found", Data = null });
            }  
        }
    }
}

