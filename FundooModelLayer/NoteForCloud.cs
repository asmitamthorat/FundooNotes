using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

namespace FundooModelLayer
{
    public class NoteForCloud
    {  
        public bool IsPin { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
       public IFormFile Image { get; set; }
        public DateTime? Remainder { get; set; }
    }
}
