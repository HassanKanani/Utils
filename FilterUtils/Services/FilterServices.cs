using FilterUtils.FilterModels;
using System.Linq.Expressions;
using Utils.Pagination;
namespace FilterUtils.Services;
public class FilterServices
{
    private readonly SortService _sortService;

    public FilterServices(SortService sortService)
    {
        _sortService = sortService;
    }

    public PagedResult<T> ApplyDynamicFilter<T>(IQueryable<T> query, Filter filter)
    {
        query = filter is not null
            ? ApplyFilters(query, filter)
            : query;

        return query.GetPaged(filter?.Page ?? 1, filter?.PageSize ?? 25);
    }

    private IQueryable<T> ApplyFilters<T>(IQueryable<T> query, Filter filter)
    {
        if (filter.DynamicFilterParams is not null)
        {
            foreach (var filterItem in filter.DynamicFilterParams.FilterItems)
            {
                query = filterItem.Value is not null
                    ? ApplyFilterItem(query, filterItem)
                    : query;
            }
        }

        // اعمال مرتب‌سازی در صورت وجود
        if (filter.SortParams is not null)
        {
            query = _sortService.ApplySort(query, filter.SortParams);
        }

        return query;
    }

    private IQueryable<T> ApplyFilterItem<T>(IQueryable<T> query, FilterItem filterItem)
    {
        var property = typeof(T).GetProperty(filterItem.Name);
        if (property != null)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var constant = Expression.Constant(Convert.ChangeType(filterItem.Value, property.PropertyType));
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
}

