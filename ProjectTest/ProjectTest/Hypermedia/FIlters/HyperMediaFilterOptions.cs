using ProjectTest.Hypermedia.Abstract;

namespace ProjectTest.Hypermedia.FIlters
{
    public class HyperMediaFilterOptions
    {
        public List<IResponseEnricher> ContentResponseEnricherList { get; set; } = new List<IResponseEnricher>();
    }
}
