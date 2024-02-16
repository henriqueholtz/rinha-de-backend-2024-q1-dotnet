using Microsoft.AspNetCore.Mvc;
using RinhaBackend.Api.DTO;

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
            return Ok($"customerId: {customerId}");
        }

        // TODO: Accept only integers in customerId
        [HttpPost("/clientes/{customerId}/transacoes", Name = "InsertTransaction")]
        public IActionResult InsertTransaction(int customerId, [FromBody] TransactionDTO dto)
        {
            return Ok(new {dto, customerId});
        }
    }
}
