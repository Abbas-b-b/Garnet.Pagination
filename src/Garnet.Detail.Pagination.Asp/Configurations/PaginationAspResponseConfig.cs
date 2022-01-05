namespace Garnet.Detail.Pagination.Asp.Configurations;

/// <summary>
/// The configuration for response fields
/// </summary>
public class PaginationAspResponseConfig
{
    /// <summary>
    /// Header field name for total number of elements, if used
    /// </summary>
    public string HeaderTotalNumberOfElementFieldName { get; set; } = "X-Total-Count";
}