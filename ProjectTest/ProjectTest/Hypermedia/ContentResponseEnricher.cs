using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using ProjectTest.Hypermedia.Abstract;
using ProjectTest.Hypermedia.Utils;
using System.Collections.Concurrent;

namespace ProjectTest.Hypermedia
{
    public abstract class ContentResponseEnricher<T> : IResponseEnricher where T : ISupportsHyperMedia
    {
        public ContentResponseEnricher()
        {

        }

        public bool CanEnrich(Type contentType)
        {
            return contentType == typeof(T) || contentType == typeof(PagesSearchVO<T>);
        }

        protected abstract Task EnrichModel(T content, IUrlHelper urlHelper);

        bool IResponseEnricher.CanEnrich(ResultExecutingContext response)
        {
            if (response.Result is OkObjectResult okObjectResult)
                return CanEnrich(okObjectResult.Value.GetType());

            return false;
        }

        public async Task Enrich(ResultExecutingContext response)
        {
            var urlHelper = new UrlHelperFactory().GetUrlHelper(response);

            if (response.Result is OkObjectResult okObjectResult)
            {
                if (okObjectResult.Value is T model)
                {
                    await EnrichModel(model, urlHelper);
                }
                else if (okObjectResult.Value is List<T> collection)
                {
                    ConcurrentBag<T> bag = new ConcurrentBag<T>(collection);

                    Parallel.ForEach(bag, (element) =>
                    {
                        EnrichModel(element, urlHelper);
                    });
                }
                else if (okObjectResult.Value is PagesSearchVO<T> pagesSearch)
                {

                    Parallel.ForEach(pagesSearch.List.ToList(), (element) =>
                    {
                        EnrichModel(element, urlHelper);
                    });
                }
            }

            await Task.FromResult<object>(null);
        }
    }
}
