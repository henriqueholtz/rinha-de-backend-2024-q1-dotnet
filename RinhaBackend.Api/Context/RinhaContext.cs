using Microsoft.EntityFrameworkCore;
using RinhaBackend.Api.Models;

namespace RinhaBackend.Api.Context
{
    public class RinhaContext : DbContext
    {
        public RinhaContext(DbContextOptions<RinhaContext> options) : base(options) { }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
