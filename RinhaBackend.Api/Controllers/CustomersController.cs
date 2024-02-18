using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RinhaBackend.Api.Context;
using RinhaBackend.Api.DTO.GetCustomerExtract;
using RinhaBackend.Api.DTO.InsertTransaction;
using RinhaBackend.Api.Models;

namespace RinhaBackend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController(RinhaContext context) : ControllerBase
    {
        private readonly RinhaContext _context = context;

        private async Task<List<TransactionDTO>> GetLatestTransactionsAsync(int customerId)
        {
            List<TransactionDTO> result = new List<TransactionDTO>();
            var transactions = await _context.Transactions.Where(t => t.CustomerId == customerId).OrderByDescending(o => o.Date).ToListAsync();
            foreach (var transaction in transactions)
            {
                result.Add(new TransactionDTO
                {
                    Date = transaction.Date,
                    Description = transaction.Description,
                    Type = transaction.IsCredit ? 'c' : 'd',
                    Value = transaction.Value,
                });
            }

            return result;
        }

        private decimal GetBalance(List<TransactionDTO> transactions)
        {
            decimal balance = 0;
            foreach (var transaction in transactions)
            {
                if (transaction.Type == 'c')
                    balance += transaction.Value;
                else
                    balance -= transaction.Value;
            }
            return balance;
        }

        // TODO: Accept only integers in customerId
        [HttpGet("/clientes/{customerId}/extrato", Name = "GetCustomerExtract")]
        public async Task<IActionResult> GetCustomerExtract(int customerId)
        {
            Customer? customerDb = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == customerId);
            if (customerDb == null) 
                return NotFound();

            List<TransactionDTO> latestTransactions = await GetLatestTransactionsAsync(customerId);
            var output = new OutputGetCustomerExtractDTO
            {
                Balance = new BalanceDTO
                {
                    ExtractDate = DateTime.UtcNow,
                    Limit = customerDb.MaxLimit,
                    Total = GetBalance(latestTransactions)
                },
                LatestTransactions = latestTransactions
            };
            return Ok(output);
        }

        // TODO: Accept only integers in customerId
        [HttpPost("/clientes/{customerId}/transacoes", Name = "InsertTransaction")]
        public async Task<IActionResult> InsertTransaction(int customerId, [FromBody] InputInsertTransactionDTO dto)
        {
            Customer? customerDb = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == customerId);
            if (customerDb == null)
                return BadRequest("Invalid customer");

            await _context.Transactions.AddAsync(new Transaction
            {
                CustomerId = customerId,
                Date = DateTime.UtcNow,
                Description = dto.Description,
                IsCredit = dto.Type == 'c',
                Value = dto.Value
            });
            await _context.SaveChangesAsync();
            
            List<TransactionDTO> latestTransactions = await GetLatestTransactionsAsync(customerId);

            return Ok(new OutputInsertTransactionDTO 
            { 
                Balance = GetBalance(latestTransactions),
                Limit = customerDb.MaxLimit 
            });
        }
    }
}
