using System;

namespace Garnet.Detail.Pagination.ListExtensions.Exceptions;

/// <summary>
/// Invalid order type literal
/// </summary>
public class InvalidOrderTypeException : Exception
{
    /// <summary>
    /// Invalid order type literal
    /// </summary>
    /// <param name="orderType">The invalid order type literal</param>
    public InvalidOrderTypeException(string orderType) : base($"Order type {orderType} is not valid")
    {
    }
}