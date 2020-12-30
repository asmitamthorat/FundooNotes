using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FundooModelLayer
{
    public class Collaborator
    {
        [Key]
        public int CollaboratorId { get; set; }
        public string RecieverEmail { get; set; }
        public string SenderEmail { get; set; }
        public int NoteId { get; set; }
        [ForeignKey("NoteId")]
        public virtual Note Note { get; set; }
    }
}
