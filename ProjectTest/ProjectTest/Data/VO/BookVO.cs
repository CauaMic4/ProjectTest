using System.Text.Json.Serialization;

namespace ProjectTest.Data.VO
{
    public class BookVO
    {
        //[JsonPropertyName("code")]
        public long Id { get; set; }

        //[JsonPropertyName("author")]
        public string Author { get; set; }

        //[JsonPropertyName("title")]
        public string Title { get; set; }
        public DateTime Launch_date { get; set; }
        public decimal Price { get; set; }

        [JsonIgnore]
        public bool? Ativo { get; set; }
    }
}
