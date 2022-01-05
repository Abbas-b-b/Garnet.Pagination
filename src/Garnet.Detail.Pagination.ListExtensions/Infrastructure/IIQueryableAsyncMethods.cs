using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garnet.Detail.Pagination.ListExtensions.Infrastructure;

/// <summary>
/// An interface to be able to use other libraries async methods for <see cref="ListExtensions"/> like EntityFramework
/// </summary>
public interface IIQueryableAsyncMethods
{
    /// <summary>
    /// To list asynchronously
    /// </summary>
    /// <param name="queryable">Input</param>
    /// <typeparam name="TElement">Type of <paramref name="queryable"/> and result list</typeparam>
    /// <returns>A task with result of the elements list</returns>
    Task<List<TElement>> ToListAsync<TElement>(IQueryable<TElement> queryable);

    /// <summary>
    /// Count the number of elements in <paramref name="queryable"/>
    /// </summary>
    /// <param name="queryable">Input to count the number of elements</param>
    /// <typeparam name="TElement">Type of <paramref name="queryable"/></typeparam>
    /// <returns>Number of elements in <paramref name="queryable"/></returns>
    Task<long> LongCountAsync<TElement>(IQueryable<TElement> queryable);
}