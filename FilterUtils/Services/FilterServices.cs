using FilterUtils.FilterModels;
using FilterUtils.FilterModels.Sort;
using System.Linq.Expressions;
using Utils.Models.Common;
using Utils.Pagination;
namespace FilterUtils.Services;
public class FilterServices<T>
{

    private readonly QueryableExtensions<T> queryableExtensions1;

    public FilterServices(QueryableExtensions<T> queryableExtensions1)
    {
        this.queryableExtensions1 = queryableExtensions1;
    }

    public PagedResult<T> ApplyDynamicFilter(IQueryable<T> query, Filter? filter)
    {
        query = filter is not null ? ApplyFilters(query, filter) : query;


        QueryableExtensions<T> queryableExtensions = new();

        return queryableExtensions.GetPaged(query, filter?.Page ?? 1, filter?.PageSize ?? 25);
    }
    public PagedResult<T> ApplyDynamicFilter(List<T> items, int page, int pageSize)
    {

        QueryableExtensions<T> queryableExtensions = new();

        return queryableExtensions.GetPaged(items, page, pageSize);
    }
    public PagedResult<T> ApplyDynamicFilter(IQueryable<T> query, int page, int pageSize)
    {
        QueryableExtensions<T> queryableExtensions = new();

        return queryableExtensions.GetPaged(query, page, pageSize);
    }
    private IQueryable<T> ApplyFilters(IQueryable<T> query, Filter filter)
    {

        if (filter.DynamicFilterParams is not null)
        {
            foreach (var filterItem in filter.DynamicFilterParams.FilterItems)
            {
                query = filterItem.Value is not null ? ApplyFilterItem(query, filterItem) : query;
            }
        }

        if (filter.SortParams is not null)
        {
            query = ApplySort(query, filter.SortParams);
        }

        return query;
    }

    private IQueryable<T> ApplyFilterItem(IQueryable<T> query, FilterItem filterItem)
    {

        var property = typeof(T).GetProperties().FirstOrDefault(c => string.Equals(c.Name, filterItem.Name, StringComparison.OrdinalIgnoreCase));
        if (property != null)
        {

            var parameter = Expression.Parameter(typeof(T), "x");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            object convertedValue;
            if (property.PropertyType == typeof(Guid))
            {
                if (!Guid.TryParse(filterItem.Value.ToString(), out Guid parsedGuid))
                {
                    throw new Exception("Invalid GUID value provided.");
                }
                convertedValue = parsedGuid;
            }
            else if (property.PropertyType.IsEnum)
            {
                convertedValue = Enum.Parse(property.PropertyType, filterItem.Value.ToString());
            }
            else
            {
                convertedValue = System.Convert.ChangeType(filterItem.Value, property.PropertyType);
            }

            var constant = Expression.Constant(convertedValue);
            Expression comparison = BuildComparison(propertyAccess, constant, filterItem.Comparison);
            var lambda = Expression.Lambda<Func<T, bool>>(comparison, parameter);
            return query.Where(lambda);
        }
        return query;
    }
    private Expression BuildComparison(Expression propertyAccess, Expression constant, ComparisonType comparisonType)
    {
        return comparisonType switch
        {
            ComparisonType.Equals => Expression.Equal(propertyAccess, constant),
            ComparisonType.NotEquals => Expression.NotEqual(propertyAccess, constant),
            ComparisonType.GreaterThan => Expression.GreaterThan(propertyAccess, constant),
            ComparisonType.GreaterThanOrEqual => Expression.GreaterThanOrEqual(propertyAccess, constant),
            ComparisonType.LessThan => Expression.LessThan(propertyAccess, constant),
            ComparisonType.LessThanOrEqual => Expression.LessThanOrEqual(propertyAccess, constant),
            ComparisonType.Contains => Expression.Call(propertyAccess, typeof(string).GetMethod("Contains", new[] { typeof(string) }), constant),
            _ => Expression.Equal(propertyAccess, constant)
        };
    }
    public IQueryable<T> ApplySort(IQueryable<T> query, SortParams sortParams)
    {
        IOrderedQueryable<T> orderedQuery = null;

        foreach (var sortItem in sortParams.SortItems)
        {
            var property = typeof(T).GetProperty(sortItem.Name);
            if (property != null)
            {
                var parameter = Expression.Parameter(typeof(T), "x");
                var propertyAccess = Expression.MakeMemberAccess(parameter, property);

                var lambda = Expression.Lambda<Func<T, object>>(Expression.Convert(propertyAccess, typeof(object)), parameter);

                if (orderedQuery == null)
                {
                    orderedQuery = sortItem.IsDescending ? query.OrderByDescending(lambda) : query.OrderBy(lambda);
                }
                else
                {
                    orderedQuery = sortItem.IsDescending ? orderedQuery.ThenByDescending(lambda) : orderedQuery.ThenBy(lambda);
                }
            }
        }

        var result = orderedQuery ?? query;

        return result;
    }

    public IQueryable<T> ApplySort(IQueryable<T> query, SortItem sortParams)
    {
        IOrderedQueryable<T> orderedQuery = null;


        var property = typeof(T).GetProperty(sortParams.Name);
        if (property != null)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);

            var lambda = Expression.Lambda<Func<T, object>>(Expression.Convert(propertyAccess, typeof(object)), parameter);

            if (orderedQuery == null)
            {
                orderedQuery = sortParams.IsDescending ? query.OrderByDescending(lambda) : query.OrderBy(lambda);
            }
            else
            {
                orderedQuery = sortParams.IsDescending ? orderedQuery.ThenByDescending(lambda) : orderedQuery.ThenBy(lambda);
            }
        }


        var result = orderedQuery ?? query;

        return result;
    }

    public PagedResult<T> ApplySortAndPagination(IQueryable<T> query, FilterParam filter)
    {

       query= ApplySort(query, filter.SortParam);
        QueryableExtensions<T> queryableExtensions = new();

        return queryableExtensions.GetPaged(query, filter?.Page ?? 1, filter?.PageSize ?? 25);
    }
}

