using System;
using Garnet.Pagination.Configurations;

namespace Garnet.Pagination.Exceptions;

/// <summary>
/// Exception for getting null <see cref="PaginationConfig"/>
/// </summary>
public class NullPaginationConfigException : Exception
{
    /// <summary>
    /// Exception for getting null <see cref="PaginationConfig"/>
    /// </summary>
    public NullPaginationConfigException() : base($"The {nameof(PaginationConfig)} cannot be null")
    {
    }
}