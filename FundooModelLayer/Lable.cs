using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FundooModelLayer
{
     public class Lable
    {
        [Key]
        public int LableId { get; set; }
        public string Lables { get; set; }
        public int NoteId { get; set; }
        [ForeignKey("NoteId")]
        public virtual Note Note { get; set; }
    }
}
