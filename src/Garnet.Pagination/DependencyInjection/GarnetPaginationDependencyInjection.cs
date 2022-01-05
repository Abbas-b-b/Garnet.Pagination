using Garnet.Pagination.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Garnet.Pagination.DependencyInjection;

/// <summary>
/// Bunch of methods to register Garnet pagination requirements
/// </summary>
public static class GarnetPaginationDependencyInjection
{
    /// <summary>
    /// Register Garnet pagination requirements to the service collection
    /// </summary>
    /// <param name="serviceCollection">The service collection to register pagination requirements to</param>
    /// <param name="configuration">To load <see cref="PaginationConfig"/> and <see cref="PaginationFilterConfig"/> and <see cref="PaginationOrderConfig"/> with <paramref name="configurationPath"/></param>
    /// <param name="configurationPath">Path to load <see cref="PaginationConfig"/> and <see cref="PaginationFilterConfig"/> from <paramref name="configuration"/>. It loads filter configuration with "Filter" and order configuration with "Order" key under <paramref name="configurationPath"/></param>
    /// <returns><paramref name="serviceCollection"/> after applied configurations</returns>
    public static IServiceCollection AddGarnetPagination(this IServiceCollection serviceCollection,
        IConfiguration configuration,
        string configurationPath = "Garnet.Pagination")
    {
        var paginationConfig = configuration.GetValue<PaginationConfig>(configurationPath);

        var paginationFilterConfig = configuration.GetValue<PaginationFilterConfig>($"{paginationConfig}:Filter");
            
        var paginationOrderConfig = configuration.GetValue<PaginationOrderConfig>($"{paginationConfig}:Order");

        return serviceCollection.AddGarnetPagination(paginationConfig, 
            paginationFilterConfig,
            paginationOrderConfig);
    }

    /// <summary>
    /// Register Garnet pagination requirements to the service collection
    /// </summary>
    /// <param name="serviceCollection">The service collection to register pagination requirements to</param>
    /// <param name="paginationConfig">To be used in pagination. Using default value if pass null</param>
    /// <param name="paginationFilterConfig">To be used for filtering data. Using default value if pass null</param>
    /// <param name="paginationOrderConfig">To be used for ordering data. Using default value if pass null</param>
    /// <returns><paramref name="serviceCollection"/> after applied configurations</returns>
    public static IServiceCollection AddGarnetPagination(this IServiceCollection serviceCollection,
        PaginationConfig paginationConfig = null,
        PaginationFilterConfig paginationFilterConfig = null,
        PaginationOrderConfig paginationOrderConfig = null)
    {
        serviceCollection.AddSingleton(paginationConfig ?? new PaginationConfig());

        serviceCollection.AddSingleton(paginationFilterConfig ?? new PaginationFilterConfig());

        serviceCollection.AddSingleton(paginationOrderConfig ?? new PaginationOrderConfig());

        return serviceCollection;
    }
}