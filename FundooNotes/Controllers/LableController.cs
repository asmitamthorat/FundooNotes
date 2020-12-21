using FundooModelLayer;
using FundooServiceLayer;
using FundooServiceLayer.TokenAuthentification;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    public class LableController:ControllerBase
    {
        private ILableService _lableService;
        public LableController(ILableService lableService)
        {
            _lableService = lableService;
        }

        [HttpPost]
        [Route("AddLable")]
        [TokenAuthenticationFilter]
        public ActionResult AddLable(int LableId,[FromBody] Lable lable)
        {
            var AccountId = Convert.ToInt32(HttpContext.Items["userId"]);
            Lable  result= _lableService.AddLable(AccountId,LableId, lable);
            return Ok(new ServiceResponse<Lable> { StatusCode = (int)HttpStatusCode.OK, Message = "successful", Data = result });
        }

        [HttpDelete]
        [Route("DeleteLable")]
        [TokenAuthenticationFilter]
        public ActionResult DeleteLable(int LableId)
        {
            var AccountId = Convert.ToInt32(HttpContext.Items["userId"]);
            int result = _lableService.DeleteLable(AccountId, LableId);
            return Ok(new ServiceResponse<int> { StatusCode = (int)HttpStatusCode.OK, Message = "successful", Data = result });
        }

    }
}

