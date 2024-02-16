using System.Text.Json.Serialization;

namespace RinhaBackend.Api.DTO
{
    // TODO: Use record instead of a normal class
    public class TransactionDTO()
    {
        [JsonPropertyName("Valor")]
        public decimal Value { get; set; }

        // TODO: Type must be "c" or "d"
        [JsonPropertyName("Tipo")]
        public char Type { get; set; }


        [JsonPropertyName("Descricao")]
        public string Description { get; set; }
    }
}
