using System;

namespace Garnet.Standard.Pagination.Exceptions;

/// <summary>
/// Exception for reaching the maximum possible page number and no more is acceptable
/// </summary>
public class MaximumPageNumberReachedException : Exception
{
    /// <summary>
    /// Exception for reaching the maximum possible page number and no more is acceptable
    /// </summary>
    public MaximumPageNumberReachedException() : base("Reached maximum allowed page number")
    {
    }
}