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

        public Collaborator AddCollaborator(int AccountId, string EmailId, Collaborator collaboratorModel) 
        {
            var result = _context.Collaborator.Add(collaboratorModel);
            _context.SaveChanges();
            return result.Entity;
        }

        public int DeleteCollaborator(int CollaboratorId) 
        {
            Collaborator collaborator = _context.Collaborator.Find(CollaboratorId);
            var result = _context.Collaborator.Remove(collaborator);
            _context.SaveChanges();
            return result.Entity.CollaboratorId;

        }

        public Collaborator GetCollaborator(int CollaboratorId) 
        {
            Collaborator collaborator = _context.Collaborator.Find(CollaboratorId);
            //var result = _context.Collaborator.Remove(collaborator);
            //_context.SaveChanges();
            return collaborator;
        }


    }
}
