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
    public class CollaboratorController:ControllerBase
    {
        private ICollaboratorService _collaboratorService;
        public CollaboratorController(ICollaboratorService collaboratorService) 
        {
            _collaboratorService = collaboratorService;
        }

        [HttpPost]
        [Route("AddCollaborator")]
        [TokenAuthenticationFilter]
        public IActionResult AddCollaborator([FromBody] Collaborator collaboratorModel)
        {
            var AccountId = Convert.ToInt32(HttpContext.Items["userId"]);
            var EmailId = Convert.ToInt32(HttpContext.Items["email"]);
            Collaborator collaborator = _collaboratorService.AddCollaborator(AccountId, EmailId, collaboratorModel);
            return Ok(new ServiceResponse<Collaborator> { StatusCode = (int)HttpStatusCode.OK, Message = "Deleted Successfully", Data = collaborator });
        }
    }
}
