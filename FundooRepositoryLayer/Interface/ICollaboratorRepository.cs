using FundooModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooRepositoryLayer
{
    public interface ICollaboratorRepository
    {
        Collaborator AddCollaborator(int AccountId, string EmailId, Collaborator collaboratorModel);
        int DeleteCollaborator(int CollaboratorId);
        Collaborator GetCollaborator(int CollaboratorId);
    }
}
