using System;

namespace Garnet.Detail.Pagination.ListExtensions.Exceptions;

/// <summary>
/// Exception for not finding any comparision operator from expression
/// </summary>
public class ComparisionOperatorNotFoundException : Exception
{
    /// <summary>
    /// Exception for not finding any comparision operator from expression
    /// </summary>
    /// <param name="expression">The literal that failed to find the comparision operator</param>
    public ComparisionOperatorNotFoundException(string expression)
        : base($"No comparision operator has been found for expression {expression}")
    {
    }
}