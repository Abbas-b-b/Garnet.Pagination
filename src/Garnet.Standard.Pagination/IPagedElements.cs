using System.Collections.Generic;

namespace Garnet.Standard.Pagination;

/// <summary>
/// Represents pagination result with a collection of <typeparamref name="TElement"/>
/// </summary>
/// <typeparam name="TElement">Type of page elements</typeparam>
public interface IPagedElements<out TElement>
{
    /// <summary>
    /// Page data set
    /// </summary>
    IReadOnlyList<TElement> Elements { get; }

    /// <summary>
    /// Pagination related to this result
    /// </summary>
    IPagination Pagination { get; }

    /// <summary>
    /// Total elements exist in the data source
    /// </summary>
    long NumberOfTotalElements { get; }

    /// <summary>
    /// Total available pages according to <see cref="NumberOfTotalElements"/> and <see cref="IPagination.PageSize"/>
    /// </summary>
    int NumberOfTotalPages { get; }

    /// <summary>
    /// Number of elements in this page
    /// </summary>
    int ThisPageSize { get; }

    /// <summary>
    /// Indicates whether the next page would have any elements or not
    /// </summary>
    bool HasNextPage { get; }

    /// <summary>
    /// Indicates whether the previous page would have any elements or not
    /// </summary>
    bool HasPreviousPage { get; }

    /// <summary>
    /// Indicates whether has the minimum possible page number
    /// </summary>
    bool IsFirstPage { get; }

    /// <summary>
    /// The negate of <see cref="HasNextPage"/>
    /// </summary>
    bool IsLastPage { get; }
}