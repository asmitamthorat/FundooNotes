using FundooModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooRepositoryLayer
{
    public class CollaboratorRepository:ICollaboratorRepository
    {

        private FundooDBContext _context;
        public CollaboratorRepository(FundooDBContext context)
        {
            _context = context;
        }

        public Collaborator AddCollaborator(int AccountId, int EmailId, Collaborator collaboratorModel) 
        {

            return collaboratorModel;
        }
    }
}
