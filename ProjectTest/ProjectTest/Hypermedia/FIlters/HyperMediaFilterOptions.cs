using ProjectTest.Hypermedia.Abstract;

namespace ProjectTest.Hypermedia.FIlters
{
    public class HyperMediaFilterOptions
    {
        public List<IReponseEnricher> ContentResponseEnricherList { get; set; } = new List<IReponseEnricher>();
    }
}
