using System;
using Garnet.Detail.Pagination.ListExtensions.Infrastructure;
using Garnet.Pagination.Configurations;

namespace Garnet.Detail.Pagination.ListExtensions.Exceptions;

/// <summary>
/// Exception for not assigning any <see cref="PaginationFilterConfig"/> to <see cref="ConfigProvider"/>
/// </summary>
public class PaginationFilterConfigNotAssignedException : Exception
{
    /// <summary>
    /// Exception for not assigning any <see cref="PaginationFilterConfig"/> to <see cref="ConfigProvider"/>
    /// </summary>
    public PaginationFilterConfigNotAssignedException()
        : base("PaginationFilterConfig not assigned. Please use 'UseGarnetPaginationIQueryable'")
    {
    }
}