using Microsoft.AspNetCore.Mvc;
using RinhaBackend.Api.DTO.GetCustomerExtract;
using RinhaBackend.Api.DTO.InsertTransaction;

namespace RinhaBackend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        // TODO: Accept only integers in customerId
        [HttpGet("/clientes/{customerId}/extrato", Name = "GetCustomerExtract")]
        public IActionResult GetCustomerExtract(int customerId)
        {
            var output = new OutputGetCustomerExtractDTO
            {
                Balance = new BalanceDTO
                {
                    ExtractDate = DateTime.Now,
                    Limit = 10000,
                    Total = -900
                },
                LatestTransactions = new List<TransactionDTO>
                {
                    new TransactionDTO
                    {
                        Date = DateTime.Now,
                        Description = "descricao",
                        Type = 'd',
                        Value = 900
                    }
                }
            };
            return Ok(output);
        }

        // TODO: Accept only integers in customerId
        [HttpPost("/clientes/{customerId}/transacoes", Name = "InsertTransaction")]
        public IActionResult InsertTransaction(int customerId, [FromBody] InputInsertTransactionDTO dto)
        {
            return Ok(new OutputInsertTransactionDTO { Balance = -900, Limit = 10000 });
        }
    }
}
