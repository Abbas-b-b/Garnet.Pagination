using System;
using Garnet.Detail.Pagination.ListExtensions.Infrastructure;

namespace Garnet.Detail.Pagination.ListExtensions.Exceptions;

/// <summary>
/// Exception for not assigning any <see cref="IIQueryableAsyncMethods"/> to <see cref="ConfigProvider"/>
/// </summary>
public class QueryableAsyncMethodProviderNotAssignedException : Exception
{
    /// <summary>
    /// Exception for not assigning any <see cref="IIQueryableAsyncMethods"/> to <see cref="ConfigProvider"/>
    /// </summary>
    public QueryableAsyncMethodProviderNotAssignedException()
        : base("IQueryableAsyncMethodProvider not assigned. Please use 'UseGarnetPaginationIQueryable'")
    {
    }
}