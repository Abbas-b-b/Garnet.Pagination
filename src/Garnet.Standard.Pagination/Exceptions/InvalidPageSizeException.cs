using System;

namespace Garnet.Standard.Pagination.Exceptions;

/// <summary>
/// Exception for invalid page size using
/// </summary>
public class InvalidPageSizeException : Exception
{
    /// <summary>
    /// Exception for invalid page size using
    /// </summary>
    /// <param name="invalidPageSizeValue">The invalid value that this exception occurred on</param>
    /// <param name="minimumPageSize">Minimum allowed page size</param>
    /// <param name="maximumPageSize">Maximum allowed page size</param>
    public InvalidPageSizeException(int invalidPageSizeValue, int minimumPageSize, int maximumPageSize)
        : base($"Page size {invalidPageSizeValue} is invalid. Page size should be between {minimumPageSize} and {maximumPageSize}")
    {
    }
}