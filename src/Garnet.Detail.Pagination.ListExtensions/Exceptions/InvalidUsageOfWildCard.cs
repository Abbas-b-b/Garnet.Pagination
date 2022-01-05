using System;

namespace Garnet.Detail.Pagination.ListExtensions.Exceptions;

/// <summary>
/// Invalid usage of wild card
/// </summary>
public class InvalidUsageOfWildCard : Exception
{
    /// <summary>
    /// Invalid usage of wild card
    /// </summary>
    /// <param name="expression">The expression that invalid wild card used in</param>
    /// <param name="wildCardSign">The wild card sign that tried to parse the expression with</param>
    public InvalidUsageOfWildCard(string expression, string wildCardSign)
        : base($"Invalid usage of wildcard {wildCardSign} in expression {expression}")
    {
    }
}