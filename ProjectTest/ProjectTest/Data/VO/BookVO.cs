using ProjectTest.Hypermedia;
using ProjectTest.Hypermedia.Abstract;
using System.Text.Json.Serialization;

namespace ProjectTest.Data.VO
{
    public class BookVO : ISupportsHyperMedia
    {
        public long Id { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public DateTime Launch_date { get; set; }
        public decimal Price { get; set; }

        [JsonIgnore]
        public bool? Ativo { get; set; }
        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}
