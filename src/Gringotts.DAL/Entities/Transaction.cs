using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gringotts.DAL.Entities
{
    public class Transaction
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("account_number")]
        public int AccountNumber { get; set; }

        [Column("amount")]
        public string Amount { get; set; }

        [Column("currency")]
        public string Currency { get; set; }

        [Column("balance")]
        public string Balance { get; set; } // stringified money object

        [Column("type")]
        public string Type { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column("time")]
        public DateTime Time { get; set; }

        //Mapping
        [ForeignKey("account_number")]
        public virtual Account Account { get; set; }
    }
}
