namespace Garnet.Standard.Pagination;

/// <summary>
/// Pagination information for a specific page can be used to retrieve page elements from the data source
/// </summary>
public interface IPagination
{
    /// <summary>
    /// This page number
    /// </summary>
    /// <exception cref="Exceptions.InvalidPageNumberException">When page number is not between start page and maximum allowed page number according to the provided configurations</exception>
    int PageNumber { get; }

    /// <summary>
    /// This page size
    /// </summary>
    /// <exception cref="Exceptions.InvalidPageSizeException">When page size is not between minimum and maximum allowed page size</exception>
    int PageSize { get; }

    /// <summary>
    /// Conditional filter string according to the provided filter configuration
    /// </summary>
    string Filters { get; }

    /// <summary>
    /// Field name to order by, according to the provided order configuration
    /// </summary>
    string Orders { get; }

    /// <summary>
    /// <para>Number of elements before this page</para> 
    /// <para>If <see cref="PageNumber"/> starts from 0 then <see cref="PageNumber"/> * <see cref="PageSize"/></para>
    /// <para>If <see cref="PageNumber"/> starts from 1 then (<see cref="PageNumber"/> - 1) * <see cref="PageSize"/></para>
    /// </summary>
    long Offset { get; }

    /// <summary>
    /// To build a <see cref="IPagination"/> for the next page according to this page
    /// </summary>
    /// <returns><see cref="IPagination"/> for the next page</returns>
    /// <exception cref="Exceptions.MaximumPageNumberReachedException">When next page is not available due to reaching the maximum allowed page number</exception>
    IPagination GetNextPagePagination();

    /// <summary>
    /// To build a <see cref="IPagination"/> for the previous page according to this page
    /// </summary>
    /// <returns><see cref="IPagination"/> for the previous page</returns>
    /// <exception cref="Exceptions.MinimumPageNumberReachedException">When previous page is not available due to reaching the minimum allowed number</exception>
    IPagination GetPreviousPagePagination();
}