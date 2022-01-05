using System;
using System.Linq;

namespace Garnet.Detail.Pagination.ListExtensions.Operators;

/// <summary>
/// Represents an operator with a sign that can be applied on an expression
/// </summary>
internal interface IOperator
{
    /// <summary>
    /// Try to parse the expression literal for this operator
    /// </summary>
    /// <param name="expression">The expression literal to parse</param>
    /// <param name="operands">If parse was successful, left and right operands will be filed</param>
    /// <returns>Whether parsing the <paramref name="expression"/> was successful or not</returns>
    bool TryParseExpression(string expression, out Tuple<string, string> operands);

    /// <summary>
    /// To apply the operator on <paramref name="queryable"/>
    /// </summary>
    /// <param name="queryable">The input to apply this operator with <paramref name="expression"/></param>
    /// <param name="expression">The expression to get operands to apply this operator on <paramref name="queryable"/></param>
    /// <typeparam name="T">Type of <paramref name="queryable"/> and output <see cref="ListExtensions"/></typeparam>
    /// <returns></returns>
    IQueryable<T> Apply<T>(IQueryable<T> queryable, string expression) where T : class;
}