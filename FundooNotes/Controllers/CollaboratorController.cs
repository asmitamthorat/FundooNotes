using FundooModelLayer;
using FundooServiceLayer;
using FundooServiceLayer.MSMQService;
using FundooServiceLayer.TokenAuthentification;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace FundooNotes.Controllers
{
    public class CollaboratorController:ControllerBase
    {
        private ICollaboratorService _collaboratorService;
        private IMSMQ _msmq;
        public CollaboratorController(ICollaboratorService collaboratorService,IMSMQ msmq) 
        {
            _collaboratorService = collaboratorService;
            _msmq = msmq;
        }

        [HttpPost]
        [Route("AddCollaborator")]
        [TokenAuthenticationFilter]
        public IActionResult AddCollaborator([FromBody] Collaborator collaboratorModel)
        {
            try
            {

                var AccountId = Convert.ToInt32(HttpContext.Items["userId"]);
                collaboratorModel.SenderEmail = Convert.ToString(HttpContext.Items["email"]);
                Collaborator collaborator = _collaboratorService.AddCollaborator(AccountId, collaboratorModel.SenderEmail, collaboratorModel);
                if (collaborator == null)
                {
                    return NotFound(new ServiceResponse<Collaborator> { StatusCode = (int)HttpStatusCode.NotFound, Message = "Internal Server Error", Data = null });
                }
               // _msmq.AddToQueue(collaboratorModel.RecieverEmail + " " + "Collaborated Successfully by" + collaboratorModel.SenderEmail + "  " + System.DateTime.Now.ToString());
                return Ok(new ServiceResponse<Collaborator> { StatusCode = (int)HttpStatusCode.OK, Message = "Added Successfully", Data = collaborator });

            }
            catch (Exception)
            {
                return BadRequest(new ServiceResponse<Collaborator> { StatusCode = (int)HttpStatusCode.BadRequest, Message = "Page Not Found", Data = null });

            }

        }

        [HttpDelete]
        [Route("DeleteCollaborator")]
        [TokenAuthenticationFilter]
        public IActionResult DeleteCollaborator(int CollaboratorId)
        {
            try
            {
                int result = _collaboratorService.DeleteCollaborator(CollaboratorId);
                _msmq.AddToQueue(result + " " + "Collaborator Deleted " + "  " + System.DateTime.Now.ToString());
                return Ok(new ServiceResponse<int> { StatusCode = (int)HttpStatusCode.OK, Message = "Added Successfully", Data = result });
            }
            catch (Exception) 
            {
                return BadRequest(new ServiceResponse<Collaborator> { StatusCode = (int)HttpStatusCode.BadRequest, Message = "Page Not Found", Data = null });
            }
           
        }

        [HttpGet]
        [Route("GetCollaborator")]
        [TokenAuthenticationFilter]
        public IActionResult GetCollaborator(int CollaboratorId) 
        {
            try 
            {
                Collaborator result = _collaboratorService.GetCollaborator(CollaboratorId);
                if (result == null)
                {
                    return NotFound(new ServiceResponse<Collaborator> { StatusCode = (int)HttpStatusCode.NotFound, Message = "Internal Server Error", Data = null });
                }
               // _msmq.AddToQueue(result + " " + "Collaborator Deleted " + "  " + System.DateTime.Now.ToString());
                return Ok(new ServiceResponse<Collaborator> { StatusCode = (int)HttpStatusCode.OK, Message = "Added Successfully", Data = result });
            }
            catch (Exception)
            {
                return BadRequest(new ServiceResponse<Collaborator> { StatusCode = (int)HttpStatusCode.BadRequest, Message = "Page Not Found", Data = null });
            }
           
        }
    }
}
