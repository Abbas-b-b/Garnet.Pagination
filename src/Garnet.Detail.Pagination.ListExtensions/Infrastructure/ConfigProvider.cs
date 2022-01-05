using Garnet.Detail.Pagination.ListExtensions.Exceptions;
using Garnet.Pagination.Configurations;

namespace Garnet.Detail.Pagination.ListExtensions.Infrastructure;

/// <summary>
/// Make <see cref="Garnet.Pagination.Configurations.PaginationFilterConfig"/> and <see cref="Garnet.Pagination.Configurations.PaginationOrderConfig"/> and <see cref="IIQueryableAsyncMethods"/> available for using in static methods
/// </summary>
internal static class ConfigProvider
{
    private static PaginationFilterConfig _paginationFilterConfig;
    internal static PaginationFilterConfig PaginationFilterConfig
    {
        get => _paginationFilterConfig ?? throw new PaginationFilterConfigNotAssignedException();
        set => _paginationFilterConfig = value;
    }

        
    private static PaginationOrderConfig _paginationOrderConfig;
    internal static PaginationOrderConfig PaginationOrderConfig
    {
        get => _paginationOrderConfig ?? throw new PaginationOrderConfigNotAssignedException();
        set => _paginationOrderConfig = value;
    }
        

    private static IIQueryableAsyncMethods _queryableAsyncMethods;
    internal static IIQueryableAsyncMethods QueryableAsyncMethods
    {
        get => _queryableAsyncMethods ?? throw new QueryableAsyncMethodProviderNotAssignedException();
        set => _queryableAsyncMethods = value;
    }
}