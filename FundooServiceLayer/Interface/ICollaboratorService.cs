using FundooModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooServiceLayer
{
    public interface ICollaboratorService
    {
        Collaborator AddCollaborator(int AccountId, int EmailId, Collaborator collaboratorModel);
    }
}
