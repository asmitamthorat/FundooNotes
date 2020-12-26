using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FundooModelLayer
{
    public class Account
    {
        [Key]
        public int AccountId { get; set; }

        [Required]
        [Column(TypeName ="varchar(50)")]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string EmailId { get; set; }

        [Required]
        [Column(TypeName ="BigInt")]
        public long PhoneNumber { get; set; }
        public string Password { get; set; }

        public virtual List<Note> Note { get; set;}
       
    }
}
