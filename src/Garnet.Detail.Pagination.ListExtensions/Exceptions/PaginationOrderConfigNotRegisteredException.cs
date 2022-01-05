using System;
using Garnet.Pagination.Configurations;

namespace Garnet.Detail.Pagination.ListExtensions.Exceptions;

/// <summary>
/// Unable to find <see cref="PaginationOrderConfig"/> from <see cref="IServiceProvider"/>
/// </summary>
public class PaginationOrderConfigNotRegisteredException : Exception
{
    /// <summary>
    /// Unable to find <see cref="PaginationOrderConfig"/> from <see cref="IServiceProvider"/>
    /// </summary>
    public PaginationOrderConfigNotRegisteredException()
        : base("No PaginationOrderConfig has been register, Please use 'AddGarnetPagination'")
    {
    }
}