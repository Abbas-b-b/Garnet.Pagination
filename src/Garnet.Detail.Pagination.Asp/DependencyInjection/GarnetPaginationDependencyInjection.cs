using Garnet.Detail.Pagination.Asp.Configurations;
using Garnet.Pagination.DependencyInjection;
using Garnet.Pagination.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Garnet.Detail.Pagination.Asp.DependencyInjection;

/// <summary>
/// Bunch of methods to register Garnet pagination requirements for an ASP project
/// </summary>
public static class GarnetPaginationDependencyInjection
{
    /// <summary>
    /// Register Garnet pagination requirements for an ASP project to the service collection
    /// DON'T USE <see cref="Garnet.Pagination.DependencyInjection.GarnetPaginationDependencyInjection.AddGarnetPagination(Microsoft.Extensions.DependencyInjection.IServiceCollection,Microsoft.Extensions.Configuration.IConfiguration,string)"/>. THIS METHOD CALLS THAT TOO.
    /// </summary>
    /// <param name="serviceCollection">The service collection to register pagination requirements to</param>
    /// <param name="configuration">To load <see cref="PaginationConfig"/> and <see cref="PaginationFilterConfig"/> and <see cref="PaginationAspRequestConfig"/> with <paramref name="configurationPath"/></param>
    /// <param name="configurationPath">Path to load <see cref="PaginationConfig"/> and <see cref="PaginationFilterConfig"/> (with 'Filter' sub key) and <see cref="PaginationOrderConfig"/> (with 'Order' sub key) and <see cref="PaginationAspRequestConfig"/> (with 'AspRequestParameters' sub key) and <see cref="PaginationAspResponseConfig"/> (with 'AspResponseParameters' sub key) from <paramref name="configuration"/></param>
    /// <returns><paramref name="serviceCollection"/> after applied configurations</returns>
    public static IServiceCollection AddGarnetPaginationAsp(this IServiceCollection serviceCollection,
        IConfiguration configuration,
        string configurationPath = "Garnet.Pagination")
    {
        var paginationAspRequestConfig =
            configuration.GetValue<PaginationAspRequestConfig>($"{configurationPath}:AspRequestParameters");
        serviceCollection.AddSingleton(paginationAspRequestConfig);

        var paginationAspResponseConfig =
            configuration.GetValue<PaginationAspResponseConfig>($"{configurationPath}:AspResponseParameters");
        serviceCollection.AddSingleton(paginationAspResponseConfig);

        return serviceCollection.AddGarnetPagination(configuration, configurationPath);
    }

    /// <summary>
    /// Register Garnet pagination requirements for an ASP project to the service collection
    /// DON'T USE <see cref="Garnet.Pagination.DependencyInjection.GarnetPaginationDependencyInjection.AddGarnetPagination(Microsoft.Extensions.DependencyInjection.IServiceCollection,Microsoft.Extensions.Configuration.IConfiguration,string)"/>. THIS METHOD CALLS THAT TOO.
    /// </summary>
    /// <param name="serviceCollection">The service collection to register pagination requirements to</param>
    /// <param name="paginationConfig">To be used in pagination. Using default value if pass null</param>
    /// <param name="paginationFilterConfig">To be used for filtering data. Using default value if pass null</param>
    /// <param name="paginationOrderConfig">To be used for ordering data. Using default value if pass null</param>
    /// <param name="paginationAspRequestConfig">To be used for mapping incoming requests to the corresponding object. Using default value if pass null</param>
    /// <param name="paginationAspResponseConfig">To be used for exposing pagination result. Using default value if pass null</param>
    /// <returns><paramref name="serviceCollection"/> after applied configurations</returns>
    public static IServiceCollection AddGarnetPaginationAsp(this IServiceCollection serviceCollection,
        PaginationConfig paginationConfig = null,
        PaginationFilterConfig paginationFilterConfig = null,
        PaginationOrderConfig paginationOrderConfig = null,
        PaginationAspRequestConfig paginationAspRequestConfig = null,
        PaginationAspResponseConfig paginationAspResponseConfig = null)
    {
        serviceCollection.AddSingleton(paginationAspRequestConfig ?? new PaginationAspRequestConfig());
        serviceCollection.AddSingleton(paginationAspResponseConfig ?? new PaginationAspResponseConfig());

        return serviceCollection.AddGarnetPagination(paginationConfig,
            paginationFilterConfig,
            paginationOrderConfig);
    }
}