using System;
using Garnet.Detail.Pagination.ListExtensions.Exceptions;
using Garnet.Detail.Pagination.ListExtensions.Infrastructure;
using Garnet.Pagination.Configurations;
using Microsoft.AspNetCore.Builder;

namespace Garnet.Detail.Pagination.ListExtensions.DependencyInjection;

/// <summary>
/// Bunch of methods to make Garnet pagination IQueryable functional
/// </summary>
public static class GarnetPaginationDependencyInjection
{
    /// <summary>
    /// Configure Garnet pagination IQueryable by loading <see cref="PaginationFilterConfig"/> and <see cref="PaginationOrderConfig"/> from registered services
    /// </summary>
    /// <param name="applicationBuilder">To get registered services from <see cref="IApplicationBuilder.ApplicationServices"/></param>
    /// <param name="queryableAsyncMethods">Async methods for <see cref="ListExtensions"/>. Using default methods if pass null</param>
    /// <returns><paramref name="applicationBuilder"/></returns>
    /// <exception cref="PaginationFilterConfigNotRegisteredException"></exception>
    /// <exception cref="PaginationOrderConfigNotRegisteredException"></exception>
    public static IApplicationBuilder UseGarnetPaginationListExtensions(this IApplicationBuilder applicationBuilder,
        IIQueryableAsyncMethods queryableAsyncMethods = null)
    {
        applicationBuilder.ApplicationServices.UseGarnetPaginationListExtensions(queryableAsyncMethods);

        return applicationBuilder;
    }

    /// <summary>
    /// Configure Garnet pagination IQueryable by loading <see cref="PaginationFilterConfig"/> and <see cref="PaginationOrderConfig"/> from registered services
    /// </summary>
    /// <param name="serviceProvider">To get registered services from</param>
    /// <param name="queryableAsyncMethods">Async methods for <see cref="ListExtensions"/>. Using default methods if pass null</param>
    /// <returns><paramref name="serviceProvider"/></returns>
    /// <exception cref="PaginationFilterConfigNotRegisteredException"></exception>
    /// <exception cref="PaginationOrderConfigNotRegisteredException"></exception>
    public static IServiceProvider UseGarnetPaginationListExtensions(this IServiceProvider serviceProvider,
        IIQueryableAsyncMethods queryableAsyncMethods = null)
    {
        if (serviceProvider.GetService(typeof(PaginationFilterConfig)) is not PaginationFilterConfig filterConfig)
        {
            throw new PaginationFilterConfigNotRegisteredException();
        }

        if (serviceProvider.GetService(typeof(PaginationOrderConfig)) is not PaginationOrderConfig orderConfig)
        {
            throw new PaginationOrderConfigNotRegisteredException();
        }

        UseGarnetPaginationListExtensions(filterConfig, orderConfig, queryableAsyncMethods);

        return serviceProvider;
    }

    /// <summary>
    /// Configure Garnet pagination IQueryable by assigning <paramref name="paginationFilterConfig"/> and <paramref name="paginationOrderConfig"/> and <paramref name="queryableAsyncMethods"/>
    /// Default configurations would be used if null passed.
    /// </summary>
    /// <param name="paginationFilterConfig">To be used for filtering data. Using default value if pass null</param>
    /// <param name="paginationOrderConfig">To be used for ordering data. Using default value if pass null</param>
    /// <param name="queryableAsyncMethods">Async methods for <see cref="ListExtensions"/>. Using default methods if pass null</param>
    public static void UseGarnetPaginationListExtensions(PaginationFilterConfig paginationFilterConfig = null,
        PaginationOrderConfig paginationOrderConfig = null,
        IIQueryableAsyncMethods queryableAsyncMethods = null)
    {
        ConfigProvider.PaginationFilterConfig = paginationFilterConfig ?? new PaginationFilterConfig();

        ConfigProvider.PaginationOrderConfig = paginationOrderConfig ?? new PaginationOrderConfig();

        ConfigProvider.QueryableAsyncMethods = queryableAsyncMethods ?? new DefaultIQueryableAsyncMethods();
    }
}