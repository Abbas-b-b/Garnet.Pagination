namespace Garnet.Pagination.Configurations;

/// <summary>
/// Pagination filter symbol configurations with default values
/// </summary>
/// <remarks>
/// This configuration does not exist in the standard package because it's detail related.
/// </remarks>
public class PaginationFilterConfig
{
    /// <summary>
    /// For separating filter expressions in the filter string with default sign of '&amp;&amp;'
    /// </summary>
    public string FilterExpressionSeparatorSign { get; set; } = "&&";
        
    /// <summary>
    /// With default sign of '>='
    /// </summary>
    public string GreaterThanOrEqualSign { get; set; } = ">=";

    /// <summary>
    /// With default sign of '&lt;='
    /// </summary>
    public string LessThanOrEqualSign { get; set; } = "<=";

    /// <summary>
    /// With default sign of '=='
    /// </summary>
    public string EqualSign { get; set; } = "==";

    /// <summary>
    /// With default sign of '!='
    /// </summary>
    public string NotEqualSign { get; set; } = "!=";

    /// <summary>
    /// With default sign of  '>'
    /// </summary>
    public string GreaterThanSign { get; set; } = ">";

    /// <summary>
    /// With default sign of '&lt;'
    /// </summary>
    public string LessThanSign { get; set; } = "<";

    /// <summary>
    /// With default sign of '::'
    /// </summary>
    public string LikesSign { get; set; } = "::";

    /// <summary>
    /// With default sign of '%'
    /// </summary>
    public string ZeroOrMoreCharactersWildCardSign { get; set; } = "%";

    /// <summary>
    /// With default sign of '[]'
    /// </summary>
    /// <remarks>Equals to any of the value</remarks>
    public string InListSign { get; set; } = "[]";

    /// <summary>
    /// With default sign of ','
    /// </summary>
    /// <remarks>Separator for <see cref="InListSign"/> items</remarks>
    public string InListSeparatorSign { get; set; } = ",";
}