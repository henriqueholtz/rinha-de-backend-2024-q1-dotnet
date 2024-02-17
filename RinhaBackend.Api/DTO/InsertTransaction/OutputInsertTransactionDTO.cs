using System.Text.Json.Serialization;

namespace RinhaBackend.Api.DTO.InsertTransaction
{
    public class OutputInsertTransactionDTO
    {
        [JsonPropertyName("saldo")]
        public int Balance { get; set; }

        [JsonPropertyName("limite")]
        public int Limit { get; set; }
    }
}
