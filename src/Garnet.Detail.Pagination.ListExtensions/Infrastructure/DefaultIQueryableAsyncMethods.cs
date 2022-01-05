using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garnet.Detail.Pagination.ListExtensions.Infrastructure;

internal class DefaultIQueryableAsyncMethods : IIQueryableAsyncMethods
{
    public Task<List<TElement>> ToListAsync<TElement>(IQueryable<TElement> queryable)
    {
        return Task.Run(queryable.ToList);
    }

    public Task<long> LongCountAsync<TElement>(IQueryable<TElement> queryable)
    {
        return Task.Run(queryable.LongCount);
    }
}