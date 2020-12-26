using FundooModelLayer;
using FundooRepositoryLayer;
using FundooServiceLayer.EmailService;
using FundooServiceLayer.MSMQService;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooServiceLayer
{
    public class CollaboratorService:ICollaboratorService
    {
        private ICollaboratorRepository _CollaboratorRepository;
        private INotesRepository _notesRepository;
       
      
        private IMSMQForMail _mSMQForMail;
        
        public CollaboratorService(ICollaboratorRepository CollaboratorRepository, INotesRepository notesRepository,IMSMQForMail mSMQForMail)
        {
            _CollaboratorRepository = CollaboratorRepository;
            _notesRepository = notesRepository;
         
           
            _mSMQForMail = mSMQForMail;

        }

        public Collaborator AddCollaborator(int AccountId, string EmailId, Collaborator collaboratorModel) 
        {
            

            Collaborator collaborator= _CollaboratorRepository.AddCollaborator(AccountId, EmailId, collaboratorModel);
            Note note =  _notesRepository.GetNote(AccountId,collaboratorModel.NoteId );
            Message message = new EmailService.Message(new string[] { collaboratorModel.RecieverEmail },
               "Added as collaborator",
               $"<h2>You have been added as collaborated by <p style='color:red'>" + collaboratorModel.SenderEmail + "</p> To note <p style='color:green'>" + note.Title + "</p><h2>");
            // _emailSender.SendEmail(message);
         
            _mSMQForMail.AddToQueue(message);



            return collaborator;
        }

        public int DeleteCollaborator(int CollaboratorId)
        {
            return _CollaboratorRepository.DeleteCollaborator(CollaboratorId);
        }


        public Collaborator GetCollaborator(int CollaboratorId)
        {
            return _CollaboratorRepository.GetCollaborator(CollaboratorId);
        }


    }
}
