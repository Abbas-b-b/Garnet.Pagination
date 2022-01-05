using System;

namespace Garnet.Standard.Pagination.Exceptions;

/// <summary>
/// Exception for reaching minimum allowed page number and no less page number is available
/// </summary>
public class MinimumPageNumberReachedException : Exception
{
    /// <summary>
    /// Exception for reaching minimum allowed page number and no less page number is available
    /// </summary>
    public MinimumPageNumberReachedException() : base("Page number is the minimum allowed value")
    {
    }
}