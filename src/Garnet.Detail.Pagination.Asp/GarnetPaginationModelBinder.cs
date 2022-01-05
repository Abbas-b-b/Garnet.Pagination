using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Garnet.Detail.Pagination.Asp.Configurations;
using Garnet.Detail.Pagination.Asp.Exceptions;
using Garnet.Pagination.Configurations;
using Garnet.Standard.Pagination;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;

namespace Garnet.Detail.Pagination.Asp;

/// <summary>
/// A model binder to map the incoming request to <see cref="IPagination"/> or <see cref="Garnet.Pagination.Pagination"/>
/// </summary>
public class GarnetPaginationModelBinder : IModelBinder
{
    private readonly PaginationAspRequestConfig _paginationAspRequestConfig;
    private readonly PaginationConfig _paginationConfig;

    /// <summary>
    /// A model binder to map the incoming request to <see cref="IPagination"/> or <see cref="Garnet.Pagination.Pagination"/>
    /// </summary>
    /// <param name="paginationAspRequestConfig">Configuration used to map incoming request to <see cref="IPagination"/> or <see cref="Garnet.Pagination.Pagination"/></param>
    /// <param name="paginationConfig">A configuration that is shared across all created instance of <see cref="IPagination"/> or <see cref="Garnet.Pagination.Pagination"/></param>
    public GarnetPaginationModelBinder(PaginationAspRequestConfig paginationAspRequestConfig,
        PaginationConfig paginationConfig)
    {
        _paginationAspRequestConfig = paginationAspRequestConfig;
        _paginationConfig = paginationConfig;
    }

    /// <summary>
    /// Bind incoming request parameter to an instance of <see cref="IPagination"/> or <see cref="Garnet.Pagination.Pagination"/>
    /// </summary>
    /// <param name="bindingContext">Provided by ASP</param>
    /// <returns>A <see cref="Task"/> which will complete when the model binding process completes.</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="bindingContext"/> is null</exception>
    /// <exception cref="MultiPaginationParametersNotSupportedException">When the target action has multiple parameter of type <see cref="IPagination"/> or <see cref="Garnet.Pagination.Pagination"/></exception>
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
        {
            throw new ArgumentNullException(nameof(bindingContext));
        }

        if (bindingContext.ActionContext.ActionDescriptor.Parameters.Count(descriptor =>
                descriptor.ParameterType.IsAssignableFrom(typeof(IPagination))
                || descriptor.ParameterType.IsAssignableFrom(typeof(Garnet.Pagination.Pagination)))
            > 1)
        {
            throw new MultiPaginationParametersNotSupportedException();
        }

        var pagination = CreatePaginationFromQueryString(bindingContext);

        if (pagination is not null)
        {
            bindingContext.Result = ModelBindingResult.Success(pagination);
        }

        return Task.CompletedTask;
    }

    /// <summary>
    /// Create an instance of <see cref="Garnet.Pagination.Pagination"/> from query string parameters
    /// </summary>
    /// <param name="bindingContext">The binding context to get query string parameters and add model error if could not parse query string parameters</param>
    /// <returns>Null if could not create an instance of <see cref="Garnet.Pagination.Pagination"/></returns>
    protected Garnet.Pagination.Pagination CreatePaginationFromQueryString(ModelBindingContext bindingContext)
    {
        var queryString = bindingContext.HttpContext.Request.QueryString;
            
        var parsedQueryString = QueryHelpers.ParseQuery(queryString.Value);

        var pageNumberString = GetPaginationFieldFromQueryString(parsedQueryString,
            _paginationAspRequestConfig.PageNumberParameterName,
            defaultValue: (int)_paginationConfig.StartPageNumber);
        var pageNumber = Cast<int>(pageNumberString,
            bindingContext,
            _paginationAspRequestConfig.PageNumberParameterName);

        var pageSizeString = GetPaginationFieldFromQueryString(parsedQueryString,
            _paginationAspRequestConfig.PageSizeParameterName,
            defaultValue: _paginationConfig.DefaultPageSize);
        var pageSize = Cast<int>(pageSizeString, bindingContext, _paginationAspRequestConfig.PageSizeParameterName);

        var filters = GetPaginationFieldFromQueryString(parsedQueryString,
            _paginationAspRequestConfig.FilterParameterName,
            defaultValue: "");

        var orders = GetPaginationFieldFromQueryString(parsedQueryString,
            _paginationAspRequestConfig.OrderParameterName,
            defaultValue: "");

        if (pageNumber is null || pageSize is null)
        {
            return null;
        }

        if (pageNumber < (int)_paginationConfig.StartPageNumber || pageNumber > _paginationConfig.MaxPageNumber)
        {
            bindingContext.ModelState.TryAddModelError(_paginationAspRequestConfig.PageNumberParameterName,
                $"{pageNumber} is not valid, should be between {(int)_paginationConfig.StartPageNumber} and {_paginationConfig.MaxPageNumber}");

            return null;
        }

        if (pageSize < 1 || pageNumber > _paginationConfig.MaxPageSize)
        {
            bindingContext.ModelState.TryAddModelError(_paginationAspRequestConfig.PageSizeParameterName,
                $"{pageSize} is not valid, should be between 1 and {_paginationConfig.MaxPageSize}");

            return null;
        }

        return new Garnet.Pagination.Pagination(_paginationConfig,
            pageNumber.Value,
            pageSize.Value,
            filters as string,
            orders as string
        );
    }

    /// <summary>
    /// Get a pagination field from a dictionary of query strings
    /// </summary>
    /// <param name="parsedQueryString">List of all query string parameters</param>
    /// <param name="fieldName">Query string parameter field name</param>
    /// <param name="defaultValue">Default value if the parameter does not exist in the query string</param>
    /// <returns>Found parameter value from query string or <paramref name="defaultValue"/> if the parameter does not exist in the query string</returns>
    protected object GetPaginationFieldFromQueryString(Dictionary<string, StringValues> parsedQueryString,
        string fieldName,
        object defaultValue)
    {
        if (parsedQueryString.ContainsKey(fieldName))
        {
            return parsedQueryString[fieldName].ToString();
        }

        return defaultValue;
    }

    private static T? Cast<T>(object value, ModelBindingContext bindingContext, string fieldName)
        where T : struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T>
    {
        if (value.GetType() == typeof(T))
        {
            return (T?)value;
        }

        if (value.GetType() != typeof(string))
        {
            throw new ArgumentOutOfRangeException(); //Unable to parse if value is not type of string
        }
            
        string destinationType;

        switch (default(T))
        {
            case int when int.TryParse(value as string, out var result):
                return (T)(object)result;
            case int:
                destinationType = nameof(Int32);
                break;
            case long when long.TryParse(value as string, out var result):
                return (T)(object)result;
            case long:
                destinationType = nameof(Int64);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        bindingContext.ModelState.AddModelError(fieldName, $"Unable to cast {value} to {destinationType}");

        return null;
    }
}