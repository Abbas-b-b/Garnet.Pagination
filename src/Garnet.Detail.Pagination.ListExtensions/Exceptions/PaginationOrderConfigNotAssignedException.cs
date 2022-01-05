using System;
using Garnet.Detail.Pagination.ListExtensions.Infrastructure;
using Garnet.Pagination.Configurations;

namespace Garnet.Detail.Pagination.ListExtensions.Exceptions;

/// <summary>
/// Exception for not assigning any <see cref="PaginationOrderConfig"/> to <see cref="ConfigProvider"/>
/// </summary>
public class PaginationOrderConfigNotAssignedException : Exception
{
    /// <summary>
    /// Exception for not assigning any <see cref="PaginationOrderConfig"/> to <see cref="ConfigProvider"/>
    /// </summary>
    public PaginationOrderConfigNotAssignedException() 
        : base("PaginationOrderConfig not assigned. Please use 'UseGarnetPaginationIQueryable'")
    {
        
    }
}