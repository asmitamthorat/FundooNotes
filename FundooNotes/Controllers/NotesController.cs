using FundooModelLayer;
using FundooServiceLayer;
using FundooServiceLayer.MSMQService;
using FundooServiceLayer.TokenAuthentification;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private INoteService _service;
        ITokenManager _tokenManager;
        IMSMQ _msmq;
        private IDistributedCache _distrubutedCache;
        string key = "FundooNotes";
        public NotesController(INoteService service, ITokenManager tokenManager, IMSMQ msmq, IDistributedCache distrubutedCache)
        {
            _service = service;
            _tokenManager = tokenManager;
            _msmq = msmq;
            _distrubutedCache = distrubutedCache;
        }

        [HttpGet]
        [TokenAuthenticationFilter]
        public ActionResult GetNotes()
        {
            var AccountId = Convert.ToInt32(HttpContext.Items["userId"]);
            List<NotesViewModel> notes = new List<NotesViewModel>();
            try {
                var Chache = this._distrubutedCache.GetString(key);
                if (Chache == null)
                {
                    notes = _service.GetNotes(AccountId);
                    if (notes == null)
                    {
                        return NotFound(new ServiceResponse<List<Note>> { StatusCode = (int)HttpStatusCode.NotFound, Message = "Internal Server Error", Data = null });
                    }
                    else
                    {
                        var jsonModel = JsonConvert.SerializeObject(notes);
                        this._distrubutedCache.SetString(key, jsonModel);
                        return Ok(new ServiceResponse<List<NotesViewModel>> { StatusCode = (int)HttpStatusCode.Created, Message = "successful", Data = notes });
                    }
                }
                var model = JsonConvert.DeserializeObject<List<NotesViewModel>>(Chache);
                _msmq.AddToQueue("User with AccountId " + AccountId + " " + "have seen Notes " + "  " + System.DateTime.Now.ToString());
                return Ok(new ServiceResponse<List<NotesViewModel>> { StatusCode = (int)HttpStatusCode.OK, Message = "successful", Data = model });
            }
            catch (Exception)
            {
                return BadRequest(new ServiceResponse<List<NotesViewModel>> { StatusCode = (int)HttpStatusCode.BadRequest, Message = "Page Not Found", Data = null });
            }
        }

        [HttpPost]
        //[HttpPost("upload")]
        [TokenAuthenticationFilter]
        public ActionResult AddNote([FromForm] NoteForCloud note)
        {
            

            var AccountId = Convert.ToInt32(HttpContext.Items["userId"]);
            var email = Convert.ToString(HttpContext.Items["email"]);
            try
            {
                var Chache = this._distrubutedCache.GetString(key);

                Note AddNote = _service.AddNote(AccountId, note,email);
                if (Chache == null)
                {
                    if (AddNote == null)
                    {
                        return NotFound(new ServiceResponse<List<Note>> { StatusCode = (int)HttpStatusCode.NotFound, Message = "Internal Server Error", Data = null });
                    }
                }
                else
                {
                    var Notes = JsonConvert.DeserializeObject<List<NotesViewModel>>(Chache);
                  
                    NotesViewModel note1 = new NotesViewModel {NoteId=AddNote.NoteId, Title=AddNote.Title,Description=AddNote.Description, Color = AddNote.Color,Image=AddNote.Image,IsPin=AddNote.IsPin,Remainder=AddNote.Remainder };
                    Notes.Add(note1);
                    var jsonModel = JsonConvert.SerializeObject(Notes);
                    this._distrubutedCache.SetString(key, jsonModel);
                    return Ok(new ServiceResponse<List<NotesViewModel>> { StatusCode = (int)HttpStatusCode.Created, Message = "successful", Data = Notes });
                }
                _msmq.AddToQueue(AccountId + " " + "Note Added " + "  " + System.DateTime.Now.ToString());
                this._distrubutedCache.Remove(key);
                return Ok(new ServiceResponse<Note> { StatusCode = (int)HttpStatusCode.OK, Message = "successful", Data = AddNote });
            }
            catch (Exception)
            {
                return BadRequest(new ServiceResponse<List<Note>> { StatusCode = (int)HttpStatusCode.BadRequest, Message = "Page Not Found", Data = null });
            }
        }

        [HttpGet]
        [Route("GetNote")]
        [TokenAuthenticationFilter]
        public ActionResult GetNote(int NoteId)
        {
            try {
                var Chache = this._distrubutedCache.GetString(key);
                var AccountId = Convert.ToInt32(HttpContext.Items["userId"]);
                if (Chache == null) 
                {
                    Note note1 = _service.GetNote(AccountId, NoteId);
                    if (note1 == null)
                    {
                        return NotFound(new ServiceResponse<Note> { StatusCode = (int)HttpStatusCode.NotFound, Message = "Internal Server Error", Data = null });
                    }
                    else 
                    {
                        return Ok(new ServiceResponse<Note> { StatusCode = (int)HttpStatusCode.OK, Message = "successful", Data = note1 });
                    }
                }
                var Notes=JsonConvert.DeserializeObject<List<NotesViewModel>>(Chache);
                var note=Notes.FirstOrDefault(Note => Note.NoteId == NoteId );
                return Ok(new ServiceResponse<NotesViewModel> { StatusCode = (int)HttpStatusCode.Created, Message = "successful", Data = note });
            }
            catch (Exception) 
            {
                return BadRequest(new ServiceResponse<List<Note>> { StatusCode = (int)HttpStatusCode.BadRequest, Message = "Page Not Found", Data = null });
            }
        }

        [HttpDelete]
        [TokenAuthenticationFilter]
        public ActionResult DeleteNote(int NoteId) 
        {
            int result;
            try {
                var Chache = this._distrubutedCache.GetString(key);
                var AccountId = Convert.ToInt32(HttpContext.Items["userId"]);  
                if (Chache == null)
                {
                   result = _service.DeleteNote(AccountId, NoteId);
                }
                else
                {
                    result = _service.DeleteNote(AccountId, NoteId);
                    var Notes = JsonConvert.DeserializeObject<List<NotesViewModel>>(Chache);
                    var note = Notes.FirstOrDefault(Note => Note.NoteId == NoteId);
                    Notes.Remove(note);
                    var jsonModel = JsonConvert.SerializeObject(Notes);
                    this._distrubutedCache.SetString(key, jsonModel);
                }
                return Ok(new ServiceResponse<int> { StatusCode = (int)HttpStatusCode.OK, Message = "Deleted Successfully", Data = result });
            }
            catch (Exception) 
            {
                return BadRequest(new ServiceResponse<List<int>> { StatusCode = (int)HttpStatusCode.BadRequest, Message = "Page Not Found", Data = null });
            }
        }

        [HttpPut]
        [TokenAuthenticationFilter]
        public ActionResult UpdateNote(int NoteId, [FromBody] Note note) 
        {
            try
            {
                var AccountId = Convert.ToInt32(HttpContext.Items["userId"]);
                var Chache = this._distrubutedCache.GetString(key);
                var result=new Note();
                if (Chache == null)
                {
                     result = _service.UpdateNote(AccountId, NoteId, note);
                    if (result == null)
                    {
                        return NotFound(new ServiceResponse<List<Note>> { StatusCode = (int)HttpStatusCode.NotFound, Message = "Internal Server Error", Data = null });
                    }
                }
                else
                {
                    result = _service.UpdateNote(AccountId, NoteId, note);
                    var Notes = JsonConvert.DeserializeObject<List<NotesViewModel>>(Chache);
                    var newNote = Notes.FirstOrDefault(Note => Note.NoteId == NoteId);

                    newNote.Title = note.Title;
                    newNote.Image = note.Image;
                    newNote.IsPin = note.IsPin;
                    newNote.Remainder = note.Remainder;
                    newNote.Description = note.Description;
                    newNote.Color = note.Color;
                   return Ok(new ServiceResponse<Note> { StatusCode = (int)HttpStatusCode.Created, Message = "Updated Successfully", Data = result });
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

