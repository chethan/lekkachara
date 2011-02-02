using System;
using System.ComponentModel.DataAnnotations;

namespace Service.Domain
{
    public class Transaction : Entity
    {
        public virtual User Who{ get; set; }

        [Required]
        public virtual string What { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MMM-yyyy}")]
        public virtual DateTime When { get; set; }

        [Required]
        public virtual int HowMuch { get; set; }

    }
}