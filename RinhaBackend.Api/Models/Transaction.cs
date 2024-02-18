using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RinhaBackend.Api.Models
{
    [Table("transactions")]
    public class Transaction
    {
        //[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("transactionid")]
        public int TransactionId { get; set; }

        //[ForeignKey(Customer)]
        [Column("customerid")]
        public int CustomerId { get; set; }

        [Column("value")]
        public decimal Value { get; set; }

        [Column("iscredit")]
        public bool IsCredit { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("date")]
        public DateTime Date { get; set; }
    }
}
