using FundooModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooServiceLayer
{
    public interface ICollaboratorService
    {
        Collaborator AddCollaborator(int AccountId, string EmailId, Collaborator collaboratorModel);

        int DeleteCollaborator(int CollaboratorId);

        Collaborator GetCollaborator(int CollaboratorId);
    }
}
