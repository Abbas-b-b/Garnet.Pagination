using Garnet.Detail.Pagination.Asp.ActionResults;
using Garnet.Detail.Pagination.Asp.Configurations;
using Garnet.Standard.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace Garnet.Detail.Pagination.Asp.Extensions;

/// <summary>
/// Extension methods to provide <see cref="ObjectResult"/> from <see cref="IPagedElements{TElement}"/>
/// </summary>
public static class PagedElementsObjectResultExtensions
{
    /// <summary>
    /// To <see cref="ObjectResult"/> with value of <see cref="IPagedElements{TElement}.Elements"/> from <paramref name="pagedElements"/> and a header field with name of <see cref="PaginationAspResponseConfig.HeaderTotalNumberOfElementFieldName"/> and value of <see cref="IPagedElements{TElement}.NumberOfTotalElements"/>
    /// </summary>
    /// <param name="pagedElements">PagedElements to extract values from</param>
    /// <typeparam name="TElement">Type of PagedElements</typeparam>
    /// <returns></returns>
    public static ObjectResult ToPaginationWithHeaderObjectResult<TElement>(this IPagedElements<TElement> pagedElements)
        where TElement : class
    {
        return new PaginationWithHeaderObjectResult<TElement>(pagedElements);
    }
}