using System;

namespace Garnet.Detail.Pagination.ListExtensions.Exceptions;

/// <summary>
/// No related operator found for dynamic query
/// </summary>
public class DynamicQueryOperatorSignNotFound : Exception
{
    /// <summary>
    /// No related operator found for dynamic query
    /// </summary>
    /// <param name="operatorSign">The sign literal that failed to find corresponding operator for dynamic query</param>
    public DynamicQueryOperatorSignNotFound(string operatorSign)
        : base($"No dynamic query operator sign found for operator {operatorSign}")
    {
    }
}