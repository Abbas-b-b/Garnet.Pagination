using System;

namespace Garnet.Standard.Pagination.Exceptions;

/// <summary>
/// Exception for invalid page number using
/// </summary>
public class InvalidPageNumberException : Exception
{
    /// <summary>
    /// Exception for invalid page number using
    /// </summary>
    /// <param name="invalidPageNumberValue">The invalid value that this exception occurred on</param>
    /// <param name="minimumPageNumber">Minimum allowed page number</param>
    /// <param name="maximumPageNumber">Maximum allowed page number</param>
    public InvalidPageNumberException(int invalidPageNumberValue, int minimumPageNumber, int maximumPageNumber)
        : base($"Page number {invalidPageNumberValue} is invalid. Page number should be between {minimumPageNumber} and {maximumPageNumber}")
    {
    }
}