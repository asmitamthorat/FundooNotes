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
    public class NotesController:ControllerBase
    {
        private INoteService _service;
        ITokenManager _tokenManager;
        public NotesController(INoteService service , ITokenManager tokenManager) 
        { 
            _service=service;
            _tokenManager = tokenManager;
        }

        [HttpGet]
        [Route("GetNotes")]
        [TokenAuthenticationFilter]
        public ActionResult GetNotes() 
        {
            var AccountId = Convert.ToInt32(HttpContext.Items["userId"]);
            List<Note> notes = _service.GetNotes(AccountId);
            if (notes == null) 
            {
                return NotFound(new ServiceResponse<List<Note>> { StatusCode = (int)HttpStatusCode.NotFound, Message = "", Data = null });
            }
            return Ok(new ServiceResponse<List<Note>> { StatusCode = (int)HttpStatusCode.OK, Message = "successful", Data = notes });
        }

        [HttpPost]
        [Route("AddNotes")]
        [TokenAuthenticationFilter]
        public ActionResult AddNote([FromBody] Note note) 
        {
            var AccountId = Convert.ToInt32(HttpContext.Items["userId"]);
            var AddNote = _service.AddNote(AccountId,note);
            return Ok(new ServiceResponse<Note> { StatusCode = (int)HttpStatusCode.OK, Message = "successful", Data = AddNote });
        }

        [HttpGet]
        [Route("GetNote")]
        public ActionResult GetNote(int NoteId)
        {
            var AccountId = Convert.ToInt32(HttpContext.Items["userId"]);
            Note note = _service.GetNote(AccountId, NoteId);
            return Ok(new ServiceResponse<Note> { StatusCode = (int)HttpStatusCode.OK, Message = "successful", Data = note });
        }

        [HttpDelete]
        [Route("DeleteNote")]
        [TokenAuthenticationFilter]
        public ActionResult DeleteNote(int NoteId) 
        {
            var AccountId = Convert.ToInt32(HttpContext.Items["userId"]);
            var result = _service.DeleteNote(AccountId,NoteId);
            return Ok(new ServiceResponse<int> { StatusCode = (int)HttpStatusCode.OK, Message = "Deleted Successfully", Data = result });
        }

        [HttpPut]
        [Route("UpdateNote")]
        [TokenAuthenticationFilter]
        public ActionResult UpdateNote(int NoteId, [FromBody] Note note) 
        {
            var AccountId = Convert.ToInt32(HttpContext.Items["userId"]);
            var result = _service.UpdateNote(AccountId, NoteId, note);
            return Ok(new ServiceResponse<Note> { StatusCode = (int)HttpStatusCode.OK, Message = "Updated Successfully", Data = result });
        }
    }
}
