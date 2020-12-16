using System;
using System.Collections.Generic;
using System.Text;

namespace FundooModelLayer
{
    public class Notes
    {
        public int NoteID { get; set;}
        public string Title { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public string Image { get; set; }
        public DateTime? Remainder { get; set; }
    }
}
