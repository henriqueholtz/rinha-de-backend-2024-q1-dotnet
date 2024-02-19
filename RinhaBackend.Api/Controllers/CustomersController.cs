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
            var transactions = await _context.Transactions.AsNoTracking().Where(t => t.CustomerId == customerId).OrderByDescending(o => o.Date).Take(10).ToListAsync();
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

        private int GetBalance(List<TransactionDTO> transactions)
        {
            int balance = 0;
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
            // TODO: Validate if should return 422 instead of 400
            Customer? customerDb = await _context.Customers.AsNoTracking().FirstOrDefaultAsync(c => c.CustomerId == customerId);
            if (customerDb == null)
                return BadRequest("Invalid customer");

            List<TransactionDTO> latestTransactions = await GetLatestTransactionsAsync(customerId);
            int currentBalance = GetBalance(latestTransactions);
            int currentLimit = customerDb.MaxLimit - currentBalance;
            if (currentLimit < dto.Value)
                return BadRequest("Limite ultrapassado!");

            Transaction transaction = new Transaction
            {
                CustomerId = customerId,
                Date = DateTime.UtcNow,
                Description = dto.Description,
                IsCredit = dto.Type == 'c',
                Value = dto.Value
            };
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();

            TransactionDTO transactionDTO = new TransactionDTO
            {
                Value = transaction.Value,
                Date = transaction.Date,
                Description = transaction.Description,
                Type = dto.Type
            };
            latestTransactions.Add(transactionDTO);

            return Ok(new OutputInsertTransactionDTO 
            { 
                Balance = GetBalance(latestTransactions),
                Limit = customerDb.MaxLimit 
            });
        }
    }
}
