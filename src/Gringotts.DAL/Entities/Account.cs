using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gringotts.DAL.Entities
{
    public class Account
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("account_number")]
        public int AccountNumber { get; set; }

        [Column("customer_id")]
        public int CustomerId { get; set; }

        [Required]
        [Column("type")]
        public string Type { get; set; }

        [Required]
        [Column("currency")]
        public string Currency { get; set; }

        [Required]
        [Column("balance")]
        public double Balance { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

        //Mapping
        [ForeignKey("customer_id")]
        public virtual Customer Customer { get; set; }
        public virtual List<Transaction> Transactions { get; set; }
    }
}
