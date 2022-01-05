using Garnet.Pagination.Configurations;
using Garnet.Pagination.Exceptions;
using Garnet.Standard.Pagination;
using Garnet.Standard.Pagination.Exceptions;

namespace Garnet.Pagination;

/// <inheritdoc />
public class Pagination : IPagination
{
    private readonly PaginationConfig _paginationConfig;

        
    /// <summary>
    /// Pagination information for a specific page can be used to retrieve page elements from the data source
    /// </summary>
    /// <param name="paginationConfig">The related pagination configuration
    ///     <exception cref="Exceptions.NullPaginationConfigException">When <paramref name="paginationConfig"/> is null</exception>
    /// </param>
    /// <param name="pageNumber">
    ///     Page number related to this page
    ///     <exception cref="Garnet.Standard.Pagination.Exceptions.InvalidPageNumberException">When <paramref name="pageNumber"/> is not between start page and maximum allowed page number according to the <paramref name="paginationConfig"/></exception>
    /// </param>
    /// <param name="pageSize">
    ///     Page size related to this page
    ///     <exception cref="Garnet.Standard.Pagination.Exceptions.InvalidPageSizeException"></exception>
    /// </param>
    /// <param name="filters">The pagination filter that can be used to filter on fetching data</param>
    /// <param name="orders">The pagination data order</param>
    public Pagination(PaginationConfig paginationConfig,
        int pageNumber,
        int pageSize,
        string filters = null,
        string orders = null)
    {
        _paginationConfig = paginationConfig ?? throw new NullPaginationConfigException();
            
        PageNumber = pageNumber;
        PageSize = pageSize;
        Filters = filters;
        Orders = orders;
    }

    /// <summary>
    /// Pagination information for a specific page can be used to retrieve page elements from the data source
    /// </summary>
    /// <param name="paginationConfig">
    ///     The related pagination configuration
    ///     <exception cref="Exceptions.NullPaginationConfigException">When <paramref name="paginationConfig"/> is null</exception>
    /// </param>
    public Pagination(PaginationConfig paginationConfig)
        : this(paginationConfig, (int)paginationConfig.StartPageNumber, paginationConfig.DefaultPageSize)
    {
    }


    private int _pageNumber;

    /// <inheritdoc />
    public int PageNumber
    {
        get => _pageNumber;
        protected set
        {
            if (value < (int)_paginationConfig.StartPageNumber || value > _paginationConfig.MaxPageNumber)
            {
                throw new InvalidPageNumberException(value,
                    (int)_paginationConfig.StartPageNumber,
                    _paginationConfig.MaxPageNumber);
            }

            _pageNumber = value;
        }
    }


    private int _pageSize;

    /// <inheritdoc />
    public int PageSize
    {
        get => _pageSize;
        protected set
        {
            if (value < 1 || value > _paginationConfig.MaxPageSize)
            {
                throw new InvalidPageSizeException(value, 1, _paginationConfig.MaxPageSize);
            }

            _pageSize = value;
        }
    }

    /// <inheritdoc />
    public string Filters { get; protected set; }

    /// <inheritdoc />
    public string Orders { get; protected set; }

    private long? _offset;

    /// <inheritdoc />
    public long Offset
    {
        get
        {
            if (_offset is null)
            {
                _offset = CalculateOffset();
            }

            return _offset.Value;

            long CalculateOffset()
            {
                var subtractFromPageNumber = (int)_paginationConfig.StartPageNumber;
                return (PageNumber - subtractFromPageNumber) * PageSize;
            }
        }
    }


    /// <inheritdoc />
    public IPagination GetNextPagePagination()
    {
        if (PageNumber >= _paginationConfig.MaxPageNumber)
        {
            throw new MaximumPageNumberReachedException();
        }

        return new Pagination(_paginationConfig, PageNumber + 1, PageSize, Filters, Orders);
    }

    /// <inheritdoc />
    public IPagination GetPreviousPagePagination()
    {
        if (PageNumber <= (int)_paginationConfig.StartPageNumber)
        {
            throw new MinimumPageNumberReachedException();
        }

        return new Pagination(_paginationConfig, PageNumber - 1, PageSize, Filters, Orders);
    }
}