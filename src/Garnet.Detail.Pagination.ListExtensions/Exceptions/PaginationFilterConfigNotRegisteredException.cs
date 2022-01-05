using System;
using Garnet.Pagination.Configurations;

namespace Garnet.Detail.Pagination.ListExtensions.Exceptions;

/// <summary>
/// Unable to find <see cref="PaginationFilterConfig"/> from <see cref="IServiceProvider"/>
/// </summary>
public class PaginationFilterConfigNotRegisteredException : Exception
{
    /// <summary>
    /// Unable to find <see cref="PaginationFilterConfig"/> from <see cref="IServiceProvider"/>
    /// </summary>
    public PaginationFilterConfigNotRegisteredException()
        : base("No PaginationFilterConfig has been register, Please use 'AddGarnetPagination'")
    {
    }
}