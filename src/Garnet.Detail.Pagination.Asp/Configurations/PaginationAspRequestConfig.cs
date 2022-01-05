namespace Garnet.Detail.Pagination.Asp.Configurations;

/// <summary>
/// The configuration for mapping request to the object
/// </summary>
public class PaginationAspRequestConfig
{
    /// <summary>
    /// Page number parameter name in the incoming request
    /// </summary>
    public string PageNumberParameterName { get; set; } = "page";

    /// <summary>
    /// Page size parameter name in the incoming request
    /// </summary>
    public string PageSizeParameterName { get; set; } = "size";
        
    /// <summary>
    /// Filter parameter name in the incoming request
    /// </summary>
    public string FilterParameterName { get; set; } = "filter";
        
    /// <summary>
    /// Order parameter name in the incoming request
    /// </summary>
    public string OrderParameterName { get; set; } = "order";
}