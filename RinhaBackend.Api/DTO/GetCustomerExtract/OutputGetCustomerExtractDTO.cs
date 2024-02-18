using System.Text.Json.Serialization;

namespace RinhaBackend.Api.DTO.GetCustomerExtract
{
    public class OutputGetCustomerExtractDTO
    {
        [JsonPropertyName("saldo")]
        public BalanceDTO Balance { get; set; }
        [JsonPropertyName("ultimas_transacoes")]
        public List<TransactionDTO> LatestTransactions { get; set; }
    }

    public class TransactionDTO
    {
        [JsonPropertyName("valor")]
        public decimal Value { get; set; }

        // TODO: Type must be "c" or "d"
        [JsonPropertyName("tipo")]
        public char Type { get; set; }


        [JsonPropertyName("descricao")]
        public string Description { get; set; }


        [JsonPropertyName("realizada_em")]
        public DateTime Date { get; set; }
    }

    public class BalanceDTO
    {
        public decimal Total { get; set; }

        [JsonPropertyName("data_extrato")]
        public DateTime ExtractDate { get; set; }

        [JsonPropertyName("limite")]
        public int Limit { get; set; }
    }
}
