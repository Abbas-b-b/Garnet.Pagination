using System.Threading.Tasks;
using Garnet.Detail.Pagination.Asp.Configurations;
using Garnet.Detail.Pagination.Asp.Exceptions;
using Garnet.Standard.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace Garnet.Detail.Pagination.Asp.ActionResults;

/// <summary>
/// This action result returns <see cref="IPagedElements{TElement}.Elements"/> as result and set header field with name of <see cref="PaginationAspResponseConfig.HeaderTotalNumberOfElementFieldName"/> for total elements
/// </summary>
/// <typeparam name="TPageElement">Type of page elements</typeparam>
public class PaginationWithHeaderObjectResult<TPageElement> : OkObjectResult where TPageElement : class
{
    private readonly IPagedElements<TPageElement> _pagedElements;

    /// <summary>
    /// This action result returns <see cref="IPagedElements{TElement}.Elements"/> as result and set header field with name of <see cref="PaginationAspResponseConfig.HeaderTotalNumberOfElementFieldName"/> for total elements
    /// </summary>
    /// <param name="pagedElements">A pagination result</param>
    public PaginationWithHeaderObjectResult(IPagedElements<TPageElement> pagedElements) : base(pagedElements.Elements)
    {
        _pagedElements = pagedElements;
    }

    /// <inheritdoc />
    public override void ExecuteResult(ActionContext context)
    {
        AddHeaders(context);

        base.ExecuteResult(context);
    }

    /// <inheritdoc />
    public override Task ExecuteResultAsync(ActionContext context)
    {
        AddHeaders(context);

        return base.ExecuteResultAsync(context);
    }

    private void AddHeaders(ActionContext context)
    {
        if (context.HttpContext.RequestServices.GetService(typeof(PaginationAspResponseConfig))
            is not PaginationAspResponseConfig paginationAspResponseConfig)
        {
            throw new PaginationAspResponseConfigNotRegisteredException();
        }

        context.HttpContext.Response.Headers.Add(paginationAspResponseConfig.HeaderTotalNumberOfElementFieldName,
            _pagedElements.NumberOfTotalElements.ToString());
    }
}