using System;
using System.Collections.Generic;
using Garnet.Standard.Pagination;

namespace Garnet.Pagination;

/// <inheritdoc />
public class PagedElements<TElement> : IPagedElements<TElement>
{
    /// <summary>
    /// Represents pagination result with a collection of <typeparamref name="TElement"/>
    /// </summary>
    /// <param name="pagination">The pagination used to fetch data</param>
    /// <param name="elements">Fetched data of type <typeparamref name="TElement"/></param>
    /// <param name="numberOfTotalElements">Total number of elements in the data source</param>
    public PagedElements(IPagination pagination, IReadOnlyList<TElement> elements, long numberOfTotalElements)
    {
        Pagination = pagination;
        Elements = elements;
        NumberOfTotalElements = numberOfTotalElements;
        ThisPageSize = elements.Count;
        HasNextPage = Pagination.Offset + ThisPageSize < numberOfTotalElements;
        HasPreviousPage = Pagination.Offset > 0 && numberOfTotalElements > 0;
        IsFirstPage = pagination.Offset == 0;
        IsLastPage = !HasNextPage;

        SetNumberOfTotalPages();
    }

    /// <inheritdoc />
    public IReadOnlyList<TElement> Elements { get; protected set; }

    /// <inheritdoc />
    public IPagination Pagination { get; protected set; }

    /// <inheritdoc />
    public long NumberOfTotalElements { get; protected set; }

    /// <inheritdoc />
    public int NumberOfTotalPages { get; protected set; }

    /// <inheritdoc />
    public int ThisPageSize { get; protected set; }

    /// <inheritdoc />
    public bool HasNextPage { get; protected set; }

    /// <inheritdoc />
    public bool HasPreviousPage { get; protected set; }

    /// <inheritdoc />
    public bool IsFirstPage { get; protected set; }

    /// <inheritdoc />
    public bool IsLastPage { get; protected set; }


    private void SetNumberOfTotalPages()
    {
        var totalPages = Math.Ceiling((decimal)NumberOfTotalElements / Pagination.PageSize);

        NumberOfTotalPages = (int)totalPages;
    }
}