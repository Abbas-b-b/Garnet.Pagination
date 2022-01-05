using System;

namespace Garnet.Detail.Pagination.Asp.Exceptions;

/// <summary>
/// Exception for using multiple pagination parameters in a one single method
/// </summary>
public class MultiPaginationParametersNotSupportedException : Exception
{
    /// <summary>
    /// Exception for using multiple pagination parameters in a one single method
    /// </summary>
    public MultiPaginationParametersNotSupportedException()
        : base("Multiple pagination parameters in one actions is not supported")
    {
    }
}