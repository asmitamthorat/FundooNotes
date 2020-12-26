
using Cashing;
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
    public class NotesController:ControllerBase
    {
        private INoteService _service;
        ITokenManager _tokenManager;
        IMSMQ _msmq;
        public NotesController(INoteService service , ITokenManager tokenManager,IMSMQ msmq) 
        { 
            _service=service;
            _tokenManager = tokenManager;
            _msmq = msmq;
        }

        [HttpGet]
        [Route("GetNotes")]
        [TokenAuthenticationFilter]
        [Cached(600)]
        public ActionResult GetNotes() 
        {
            try {
                var AccountId = Convert.ToInt32(HttpContext.Items["userId"]);
                List<NotesViewModel> notes = _service.GetNotes(AccountId);

                if (notes == null)
                {
                    return NotFound(new ServiceResponse<List<Note>> { StatusCode = (int)HttpStatusCode.NotFound, Message = "Internal Server Error", Data = null });
                }
                _msmq.AddToQueue("User with AccountId "+AccountId + " " + "have seen Notes " + "  " + System.DateTime.Now.ToString());
                return Ok(new ServiceResponse<List<NotesViewModel>> { StatusCode = (int)HttpStatusCode.OK, Message = "successful", Data = notes });
            }
            catch (Exception)
            {
                return BadRequest(new ServiceResponse<List<NotesViewModel>> { StatusCode = (int)HttpStatusCode.BadRequest, Message = "Page Not Found", Data = null });
            }
           
        }

        [HttpPost]
        [Route("AddNotes")]
        [TokenAuthenticationFilter]
        public ActionResult AddNote([FromBody] Note note) 
        {
            try
            {
                var AccountId = Convert.ToInt32(HttpContext.Items["userId"]);
                var AddNote = _service.AddNote(AccountId, note);
                if (AddNote == null)
                {
                    return NotFound(new ServiceResponse<List<Note>> { StatusCode = (int)HttpStatusCode.NotFound, Message = "Internal Server Error", Data = null });
                }
                _msmq.AddToQueue(AccountId + " " + "Note Added " + "  " + System.DateTime.Now.ToString());
                return Ok(new ServiceResponse<Note> { StatusCode = (int)HttpStatusCode.OK, Message = "successful", Data = AddNote });
            }
            catch (Exception)
            {
                return BadRequest(new ServiceResponse<List<Note>> { StatusCode = (int)HttpStatusCode.BadRequest, Message = "Page Not Found", Data = null });
            }
        }

        [HttpGet]
        [Route("GetNote")]
        public ActionResult GetNote(int NoteId)
        {
            try {
                var AccountId = Convert.ToInt32(HttpContext.Items["userId"]);
                Note note = _service.GetNote(AccountId, NoteId);
                if (note == null)
                {
                    return NotFound(new ServiceResponse<List<Note>> { StatusCode = (int)HttpStatusCode.NotFound, Message = "Internal Server Error", Data = null });
                }
                return Ok(new ServiceResponse<Note> { StatusCode = (int)HttpStatusCode.OK, Message = "successful", Data = note });
            }
            catch (Exception) 
            {
                return BadRequest(new ServiceResponse<List<Note>> { StatusCode = (int)HttpStatusCode.BadRequest, Message = "Page Not Found", Data = null });
            }
            
        }

        [HttpDelete]
        [Route("DeleteNote")]
        [TokenAuthenticationFilter]
        public ActionResult DeleteNote(int NoteId) 
        {
            try {
                var AccountId = Convert.ToInt32(HttpContext.Items["userId"]);
                var result = _service.DeleteNote(AccountId, NoteId);
               
                return Ok(new ServiceResponse<int> { StatusCode = (int)HttpStatusCode.OK, Message = "Deleted Successfully", Data = result });
            }
            catch (Exception) 
            {
                return BadRequest(new ServiceResponse<List<int>> { StatusCode = (int)HttpStatusCode.BadRequest, Message = "Page Not Found", Data = null });
            }
           
        }

        [HttpPut]
        [Route("UpdateNote")]
        [TokenAuthenticationFilter]
        public ActionResult UpdateNote(int NoteId, [FromBody] Note note) 
        {
            try 
            {
                var AccountId = Convert.ToInt32(HttpContext.Items["userId"]);
                var result = _service.UpdateNote(AccountId, NoteId, note);
                if (result == null)
                {
                    return NotFound(new ServiceResponse<List<Note>> { StatusCode = (int)HttpStatusCode.NotFound, Message = "Internal Server Error", Data = null });
                }
                _msmq.AddToQueue(AccountId + " " + "Note Updated " + "  " + System.DateTime.Now.ToString());
                return Ok(new ServiceResponse<Note> { StatusCode = (int)HttpStatusCode.OK, Message = "Updated Successfully", Data = result });
            }
            catch (Exception)
            {
                return BadRequest(new ServiceResponse<List<Note>> { StatusCode = (int)HttpStatusCode.BadRequest, Message = "Page Not Found", Data = null });
            }
           
        }
    }
}
