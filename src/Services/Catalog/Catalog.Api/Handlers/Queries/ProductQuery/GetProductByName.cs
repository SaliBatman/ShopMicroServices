using Catalog.Api.Data;
using Catalog.Api.Entities;
using MediatR;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Api.Handlers.Queries.ProductQuery
{
    public static class GetProductByName
    {
        //Query 
        public record Query(string name) : IRequest<Response>;

        //Handler
        public class Handler : IRequestHandler<Query, Response>
        {
            private readonly ICatalogContext _catalogContext;
            public Handler(ICatalogContext catalogContext)
            {
                _catalogContext = catalogContext;
            }
            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {

                FilterDefinition<Entities.Product> filterDefinition = Builders<Entities.Product>.Filter.ElemMatch(s => s.Name, request.name);
                var result = await _catalogContext.ProductCollection.Find(s=>s.Name == request.name).ToListAsync();
                return new Response(result);
            }
        }

        //Response

        public record Response(List<Product> product);
    }
}
