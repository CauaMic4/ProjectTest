using Microsoft.AspNetCore.Mvc.Filters;

namespace ProjectTest.Hypermedia.Abstract
{
    public interface IReponseEnricher
    {
        bool CanEnrich(ResultExecutingContext context);
        Task Enrich(ResultExecutingContext context);
    }
}
