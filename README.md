# Garnet Pagination

Dotnet library to facilitate applying pagination along with filtering and ordering to a list of objects.

---

## Garnet.Standard.Pagination [![Nuget](https://img.shields.io/nuget/dt/Garnet.Standard.Pagination?style=for-the-badge)](https://www.nuget.org/packages/Garnet.Standard.Pagination/)

    dotnet add package Garnet.Standard.Pagination

The idea behind standard packages is not to mess with the domain or application layer with implementation details and
keep it clean.

Therefore **Garnet.Standard** packages consists of abstractions.

---

## Garnet.Pagination [![Nuget](https://img.shields.io/nuget/dt/Garnet.Pagination?style=for-the-badge)](https://www.nuget.org/packages/Garnet.Pagination/)

    dotnet add package Garnet.Pagination

Implementation of the **Garnet.Standard.Pagination**

### Register to DI container

To use with [default configurations](#default-configs):

```C#
services.AddGarnetPagination();
```

Customer configurations can be used either from ```IConfiguration``` or directly passing configurations objects to other
overloads methods.

---

## Garnet.Detail.Pagination.Asp [![Nuget](https://img.shields.io/nuget/dt/Garnet.Detail.Pagination.Asp?style=for-the-badge)](https://www.nuget.org/packages/Garnet.Detail.Pagination.Asp/)

    dotnet add package Garnet.Detail.Pagination.Asp

Facilitates using pagination in web application projects by enabling getting pagination requests as an action input and
response the result after applying the pagination.

### Register to DI container

To use with default configurations:

```C#
services.AddGarnetPaginationAsp();
```

Custom configurations can be used either from ```IConfiguration``` or directly passing configurations objects to other
overloads methods.

```C#
services.AddGarnetPaginationAsp(configuration);
```

Or

```C#
services.AddGarnetPaginationAsp(paginationConfig: new PaginationConfig
    {
        DefaultPageSize = 20
    },
    paginationAspRequestConfig: new PaginationAspRequestConfig
    {
        FilterParameterName = "filter",
        OrderParameterName = "order"
    });
```

### Using in the action

```C#
[Route("[controller]")]
public class MyController : ApiController
{
    [HttpGet]
    public ActionResult<IReadOnlyList<ObjectDto>> GetAll([FromQuery] IPagination pagination)
    {  
        IPagedElements<object> objects = _objectRepository.GetAll(pagination);

        return objects.ToPaginationWithHeaderObjectResult();
    }
}
```

    http://localhost/api/my?page=1&&size=10&&filter=prop1==value&&order=id:ASC

``ToPaginationWithHeaderObjectResult()`` extension method returns an ObjectResult with the list of elements and header
field for total number of elements

---

## Garnet.Detail.Pagination.ListExtensions [![Nuget](https://img.shields.io/nuget/dt/Garnet.Detail.Pagination.ListExtensions?style=for-the-badge)](https://www.nuget.org/packages/Garnet.Detail.Pagination.ListExtensions/)

    dotnet add package Garnet.Detail.Pagination.ListExtensions

Some extension methods to use over ```IQueryable<T>``` or ``IList<T>``

This is using [Dynamic Linq](https://dynamic-linq.net/) library for filtering and ordering which means nested property (
by dot '.') filtering or ordering is also supported like `filters=prop1.prop2==value`

```C#
IQueryable<Model> model = _modelRepository.GetModels();

IPagedElements<Model> pagedModel = await model.ToPagedResultAsync();
```

### Configure

Use either of ```UseGarnetPaginationIQueryable``` overload methods to get required configurations objects from DI
already registered or pass instantly.

```C#
applicationBuilder.UseGarnetPaginationListExtensions();
```

#### IIQueryableAsyncMethods

Implement this interface to use IQueryable async extension methods from other libraries like **EntityFramework**

```C#
public class EfIQueryableAsyncMethods : IIQueryableAsyncMethods
{
    public Task<List<TElement>> ToListAsync<TElement>(IQueryable<TElement> queryable)
    {
        return queryable.ToListAsync();
    }

    public Task<long> LongCountAsync<TElement>(IQueryable<TElement> queryable)
    {
        return queryable.LongCountAsync();
    }
}
```

```C#
applicationBuilder.UseGarnetPaginationListExtensions(new EfIQueryableAsyncMethods());
```

## Default Configs

### PaginationConfig

|   Config Name   |       Default Value       |
|:---------------:|:-------------------------:|
| StartPageNumber |             1             |
| DefaultPageSize |             20            |
|  MaxPageNumber  | 2147483647 (int.MaxValue) |
|   MaxPageSize   | 2147483647 (int.MaxValue) |

### PaginationFilterConfig

|            Config Name           | Default Value |
|:--------------------------------:|:-------------:|
|   FilterExpressionSeparatorSign  |       &&      |
|      GreaterThanOrEqualSign      |       >=      |
|        LessThanOrEqualSign       |       <=      |
|             EqualSign            |       ==      |
|           NotEqualSign           |       !=      |
|          GreaterThanSign         |       >       |
|           LessThanSign           |       <       |
|             LikesSign            |       ::      |
| ZeroOrMoreCharactersWildCardSign |       %       |
|            InListSign            |       []      |
|        InListSeparatorSign       |       ,       |

### PaginationOrderConfig

|         Config Name        | Default Value |
|:--------------------------:|:-------------:|
| OrderFieldAndTypeSeparator |       :       |
|        AscendingSign       |      ASC      |
|       DescendingSign       |      DESC     |	

### PaginationAspRequestConfig

|       Config Name       | Default Value |
|:-----------------------:|:-------------:|
| PageNumberParameterName |      page     |
|  PageSizeParameterName  |      size     |
|   FilterParameterName   |     filter    |
|    OrderParameterName   |     order     |

### PaginationAspResponseConfig

|             Config Name             | Default Value |
|:-----------------------------------:|:-------------:|
| HeaderTotalNumberOfElementFieldName | X-Total-Count |