namespace Garnet.Pagination.Configurations;

/// <summary>
/// Basic paging configurations with default values if not override
/// </summary>
public class PaginationConfig
{
    /// <summary>
    /// The start page number of type <see cref="StartPageNumber"/>. 0 or 1
    /// </summary>
    public StartPageNumber StartPageNumber { get; set; } = StartPageNumber.One;

    /// <summary>
    /// Default page size if page size not specified
    /// </summary>
    public int DefaultPageSize { get; set; } = 20;

    /// <summary>
    /// Maximum allowed page number
    /// </summary>
    public int MaxPageNumber { get; set; } = int.MaxValue;

    /// <summary>
    /// Maximum allowed size for each page
    /// </summary>
    public int MaxPageSize { get; set; } = int.MaxValue;
}

/// <summary>
/// First page number indicator. 0 or 1
/// </summary>
public enum StartPageNumber : byte
{
    /// <summary>
    /// 0
    /// </summary>
    Zero = 0,
        
    /// <summary>
    /// 1
    /// </summary>
    One = 1
}