namespace Garnet.Pagination.Configurations;

/// <summary>
/// Basic configuration for pagination order with default values
/// </summary>
public class PaginationOrderConfig
{
    /// <summary>
    /// Used this sign to separate the field to apply order and order type (Ascending or Descending), with default value of ':'
    /// </summary>
    public string OrderFieldAndTypeSeparator { get; set; } = ":";

    /// <summary>
    /// Ascending order type that comes after <see cref="OrderFieldAndTypeSeparator"/>, with default value of 'ASC'
    /// </summary>
    public string AscendingSign { get; set; } = "ASC";

    /// <summary>
    /// Descending order type that comes after <see cref="OrderFieldAndTypeSeparator"/>, with default value of 'DESC'
    /// </summary>
    public string DescendingSign { get; set; } = "DESC";
}