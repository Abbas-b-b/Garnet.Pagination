using System;
using Garnet.Detail.Pagination.Asp.Configurations;

namespace Garnet.Detail.Pagination.Asp.Exceptions;

/// <summary>
/// Unable to find <see cref="PaginationAspResponseConfig"/> from <see cref="IServiceProvider"/>
/// </summary>
public class PaginationAspResponseConfigNotRegisteredException : Exception
{
    /// <summary>
    /// Unable to find <see cref="PaginationAspResponseConfig"/> from <see cref="IServiceProvider"/>
    /// </summary>
    public PaginationAspResponseConfigNotRegisteredException()
        : base("No PaginationAspResponseConfig has been register, Please use 'AddGarnetPaginationAsp'")
    {
    }
}