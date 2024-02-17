using System.ComponentModel.DataAnnotations.Schema;

namespace RinhaBackend.Api.Models
{
    [Table("customers")]
    public class Customer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("customerid")]
        public int CustomerId { get; set; }
        [Column("maxlimit")]
        public int MaxLimit { get; set; }
    }
}
