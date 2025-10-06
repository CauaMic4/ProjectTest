using Microsoft.AspNetCore.Mvc.Filters;

namespace ProjectTest.Hypermedia.Abstract
{
    public interface IReponseEnricher
    {
        bool CanEnrich(ResultExecutedContext context);
        Task Enrich(ResultExecutedContext context);
    }
}
