using System.Text.Json.Serialization;

namespace RinhaBackend.Api.DTO.InsertTransaction
{
    // TODO: Use record instead of a normal class
    public class InputInsertTransactionDTO()
    {
        [JsonPropertyName("Valor")]
        public int Value { get; set; }

        // TODO: Type must be "c" or "d"
        [JsonPropertyName("Tipo")]
        public char Type { get; set; }


        [JsonPropertyName("Descricao")]
        public string Description { get; set; }
    }
}
