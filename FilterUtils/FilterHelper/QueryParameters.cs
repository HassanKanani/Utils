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
            object convertedValue = Convert.ChangeType(filter.Value, property.Type);
            Expression valueExpression = Expression.Constant(convertedValue);
            Expression comparison = filter.Operator switch
            {
                FilterOperator.Equals => Expression.Equal(property, valueExpression),
                FilterOperator.NotEquals => Expression.NotEqual(property, valueExpression),
                FilterOperator.GreaterThan => Expression.GreaterThan(property, valueExpression),
                FilterOperator.GreaterOrEqual => Expression.GreaterThanOrEqual(property, valueExpression),
                FilterOperator.LessThan => Expression.LessThan(property, valueExpression),
                FilterOperator.LessOrEqual => Expression.LessThanOrEqual(property, valueExpression),
                FilterOperator.Contains => Expression.Call(property, typeof(string).GetMethod("Contains", new[] { typeof(string) }), valueExpression),
                _ => throw new ArgumentException("Operator نامعتبر است")
            };
            body = Expression.AndAlso(body, comparison);
        }
        FilterExpression = Expression.Lambda<Func<T, bool>>(body, parameter);
    }

    private static Expression GetPropertyExpression(Expression parameter, string propertyName)
    {
        string[] parts = propertyName.Split('.');
        Expression property = parameter;
        foreach (var part in parts)
        {
            property = Expression.Property(property, part);
        }
        return property;
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
//        var parameter = Expression.Parameter(typeof(User), "x");
//        var property = Expression.Property(parameter, queryParams.OrderBy);
//        var lambda = Expression.Lambda<Func<User, object>>(Expression.Convert(property, typeof(object)), parameter);

//        query = queryParams.OrderDescending
//            ? query.OrderByDescending(lambda)
//            : query.OrderBy(lambda);
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
//        },
//        p.RecordNumber
//    )).ToList();

//    var result = new PagedResult<UserResponse>(pagedItemsDto, totalCount, queryParams.PageNumber, queryParams.PageSize);

//    return result;

//}
