using System;

namespace Garnet.Detail.Pagination.ListExtensions.Exceptions;

/// <summary>
/// The expression is not valid
/// </summary>
public class InvalidExpressionException : Exception
{
    /// <summary>
    /// The expression is not valid
    /// </summary>
    /// <param name="expression">The invalid expression</param>
    /// <param name="operatorSign">The operator tried to parse the expression with</param>
    public InvalidExpressionException(string expression, string operatorSign)
        : base($"{expression} is not a valid expression for operator {operatorSign}")
    {
    }
}