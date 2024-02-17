using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RinhaBackend.Api.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        //[ForeignKey(Customer)]
        public int CustomerId { get; set; }
        public int Value { get; set; }
        public bool IsCredit { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
