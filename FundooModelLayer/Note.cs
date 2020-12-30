using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace FundooModelLayer
{
    public class Note
    {
        [Key]
        public int NoteId { get; set;}
        public bool IsPin { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public string Image { get; set; }
        public DateTime? Remainder { get; set; }
        public int AccountId { get; set; }
        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; }

        public virtual List<Lable> Lable { get; set; }
        public virtual List<Collaborator> Collaborator { get; set; }
    }
}
