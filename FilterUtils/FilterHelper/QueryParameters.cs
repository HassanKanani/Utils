
using System.Linq.Expressions;
using Utils.FilterHelper;
using Utils.Models;

public class QueryParameters<T>
{
    public List<FilterCriteria> Filters { get; set; } = new List<FilterCriteria>();
    public Expression<Func<T, bool>> FilterExpression { get; private set; }
    public string OrderBy { get; set; }
    public bool OrderDescending { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public void ApplyFilters()
    {
        if (Filters == null || !Filters.Any())
        {
            FilterExpression = x => true;
            return;
        }

        var parameter = Expression.Parameter(typeof(T), "x");
        Expression body = Expression.Constant(true);

        foreach (var filter in Filters)
        {
            Expression property = GetPropertyExpression(parameter, filter.PropertyName);
            var propertyType = property.Type;
            Type underlyingType = Nullable.GetUnderlyingType(propertyType) ?? propertyType;

            object convertedValue;

            if (underlyingType == typeof(Guid))
            {
                if (!Guid.TryParse(filter.Value.ToString(), out Guid guidValue))
                {
                    throw new ArgumentException($"مقدار '{filter.Value}' برای '{filter.PropertyName}' معتبر نیست.");
                }
                convertedValue = guidValue;
            }
            else if (underlyingType.IsEnum)
            {
                if (filter.Value == null)
                {
                    Expression isNull = Expression.Equal(property, Expression.Constant(null, propertyType));
                    body = Expression.AndAlso(body, isNull);
                    continue;
                }

                if (Enum.TryParse(underlyingType, filter.Value.ToString(), out var enumValue))
                {
                    convertedValue = Convert.ChangeType(enumValue, underlyingType);
                }
                else
                {
                    throw new ArgumentException($"مقدار '{filter.Value}' برای '{filter.PropertyName}' معتبر نیست.");
                }
            }
            else
            {
                convertedValue = Convert.ChangeType(filter.Value, underlyingType);
            }

            Expression valueExpression = Expression.Constant(convertedValue, propertyType);
            Expression comparison = GetComparisonExpression(property, valueExpression, filter.Operator);
            body = Expression.AndAlso(body, comparison);
        }

        FilterExpression = Expression.Lambda<Func<T, bool>>(body, parameter);
    }

    private static Expression GetComparisonExpression(Expression property, Expression valueExpression, FilterOperator filterOperator)
    {
        return filterOperator switch
        {
            FilterOperator.Equals => Expression.Equal(property, valueExpression),
            FilterOperator.NotEquals => Expression.NotEqual(property, valueExpression),
            FilterOperator.GreaterThan => Expression.GreaterThan(property, valueExpression),
            FilterOperator.GreaterOrEqual => Expression.GreaterThanOrEqual(property, valueExpression),
            FilterOperator.LessThan => Expression.LessThan(property, valueExpression),
            FilterOperator.LessOrEqual => Expression.LessThanOrEqual(property, valueExpression),
            FilterOperator.Contains when property.Type == typeof(string) =>
                Expression.Call(property, typeof(string).GetMethod("Contains", new[] { typeof(string) }), valueExpression),
            _ => throw new ArgumentException("Operator نامعتبر است")
        };
    }

    private static Expression GetPropertyExpression(Expression parameter, string propertyName)
    {
        try
        {
            string[] parts = propertyName.Split('.');
            Expression property = parameter;
            foreach (var part in parts)
            {
                property = Expression.Property(property, part);
            }
            return property;
        }
        catch
        {
            return null;
        }
    }

    public IQueryable<T> ApplyToQuery(IQueryable<T> query)
    {
        if (FilterExpression != null)
            query = query.Where(FilterExpression);

        query = ApplySorting(query, OrderBy, OrderDescending);

        return query.Skip((PageNumber - 1) * PageSize).Take(PageSize);
    }

    private static IQueryable<T> ApplySorting(IQueryable<T> query, string orderBy, bool descending)
    {
        if (string.IsNullOrWhiteSpace(orderBy))
            return query;

        var parameter = Expression.Parameter(typeof(T), "x");
        var property = GetPropertyExpression(parameter, orderBy);
        if (property == null)
            return query;

        var lambda = Expression.Lambda(property, parameter);

        string methodName = descending ? "OrderByDescending" : "OrderBy";
        var resultExpression = Expression.Call(typeof(Queryable), methodName, new Type[] { typeof(T), property.Type },
            query.Expression, Expression.Quote(lambda));

        return query.Provider.CreateQuery<T>(resultExpression);
    }
}


//public async Task<PagedResult<UserResponse>> CustomFilter(QueryParametersInputDto inputDto)
//{
//    var queryParams = new QueryParameters<User>
//    {
//        OrderBy = inputDto.OrderBy,
//        OrderDescending = inputDto.OrderDescending,
//        PageNumber = inputDto.PageNumber,
//        PageSize = inputDto.PageSize,
//        Filters = inputDto.Filters.Select(f => new FilterCriteria
//        {
//            PropertyName = f.PropertyName,
//            Operator = f.Operator,
//            Value = f.Value
//        }).ToList()
//    };

//    queryParams.ApplyFilters();

//    var query = _repository.TableNoTracking.Include(c => c.UserCategory).AsQueryable();

//    if (queryParams.FilterExpression != null)
//    {
//        query = query.Where(queryParams.FilterExpression);
//    }
//    int totalCount = query.Count();
//    if (!string.IsNullOrEmpty(queryParams.OrderBy))
//    {
//        query = queryParams.ApplyToQuery(query);
//    }

//    var allData = query.ToList();

//    var pagedItems = allData
//    .Skip((queryParams.PageNumber - 1) * queryParams.PageSize)
//    .Take(queryParams.PageSize)
//        .Select((item, index) => new PagedItem<User>(item, index + 1 + (queryParams.PageNumber - 1) * queryParams.PageSize))
//        .ToList();

//    var pagedItemsDto = pagedItems.Select(p => new PagedItem<UserResponse>(
//        new UserResponse
//        {
//            Id = p.Data.Id,
//            Name = p.Data.Name,
//            UseName = p.Data.UseName,
//            Password = p.Data.Password,
//            CategoryName = p.Data.UserCategory.Name ?? null,
//            UserStatus = p.Data.UserStatus,
//            CategoryId = p.Data.UserCategory.Id
//        },
//        p.RecordNumber
//    )).ToList();

//    var result = new PagedResult<UserResponse>(pagedItemsDto, totalCount, queryParams.PageNumber, queryParams.PageSize);

//    return result;

//}

