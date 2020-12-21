using FundooModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooRepositoryLayer
{
    public interface ICollaboratorRepository
    {
        Collaborator AddCollaborator(int AccountId, int EmailId, Collaborator collaboratorModel);
    }
}
