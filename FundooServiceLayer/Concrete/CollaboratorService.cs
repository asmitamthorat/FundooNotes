using FundooModelLayer;
using FundooRepositoryLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooServiceLayer
{
    public class CollaboratorService:ICollaboratorService
    {
        private ICollaboratorRepository _CollaboratorRepository;
        public CollaboratorService(ICollaboratorRepository CollaboratorRepository)
        {
            _CollaboratorRepository = CollaboratorRepository;
        }

        public Collaborator AddCollaborator(int AccountId, int EmailId, Collaborator collaboratorModel) 
        {
            return _CollaboratorRepository.AddCollaborator( AccountId,  EmailId, collaboratorModel);
        }


    }



}
