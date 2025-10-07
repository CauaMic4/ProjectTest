using ProjectTest.Hypermedia;
using ProjectTest.Hypermedia.Abstract;
using System.Text.Json.Serialization;

namespace ProjectTest.Data.VO
{
    public class PersonVO : ISupportsHyperMedia
    {
        [JsonPropertyName("code")]
        public long Id { get; set; }

        [JsonPropertyName("name")]
        public string FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string LastName { get; set; }
        public string Address { get; set; }

        [JsonPropertyName("sex")]
        public string Gender { get; set; }

        [JsonIgnore]
        public bool? Ativo { get; set; }
        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}
