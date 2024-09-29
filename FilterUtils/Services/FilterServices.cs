using FilterUtils.FilterModels;
using FilterUtils.FilterModels.Sort;
using System.Linq.Expressions;
namespace FilterUtils.Services;
public class FilterServices
{
    private readonly SortService _sortService;

    public FilterServices(SortService sortService)
    {
        _sortService = sortService;
    }

    public IQueryable<T> ApplyDynamicFilter<T>(IQueryable<T> query, DynamicFilter? filter, SortParams? sortParams)
    {

        if(filter !=null) {
        foreach (var FilterItem in filter.FilterItems)
        {
            if (FilterItem.Value != null)
            {
                var property = typeof(T).GetProperty(FilterItem.Name);
                if (property != null)
                {
                    var parameter = Expression.Parameter(typeof(T), "x");
                    var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                    var constant = Expression.Constant(Convert.ChangeType(FilterItem.Value, property.PropertyType));

                    // ایجاد مقایسه بر اساس نوع خاصیت و نوع مقایسه
                    Expression comparison;

                    switch (FilterItem.Comparison)
                    {
                        case ComparisonType.Equals:
                            comparison = Expression.Equal(propertyAccess, constant);
                            break;
                        case ComparisonType.NotEquals:
                            comparison = Expression.NotEqual(propertyAccess, constant);
                            break;
                        case ComparisonType.GreaterThan:
                            comparison = Expression.GreaterThan(propertyAccess, constant);
                            break;
                        case ComparisonType.GreaterThanOrEqual:
                            comparison = Expression.GreaterThanOrEqual(propertyAccess, constant);
                            break;
                        case ComparisonType.LessThan:
                            comparison = Expression.LessThan(propertyAccess, constant);
                            break;
                        case ComparisonType.LessThanOrEqual:
                            comparison = Expression.LessThanOrEqual(propertyAccess, constant);
                            break;
                        case ComparisonType.Contains:
                            comparison = Expression.Call(propertyAccess, typeof(string).GetMethod("Contains", new[] { typeof(string) }), constant);
                            break;
                        default:
                            comparison = Expression.Equal(propertyAccess, constant);
                            break;
                    }

                    var lambda = Expression.Lambda<Func<T, bool>>(comparison, parameter);
                    query = query.Where(lambda);
                }
            }
        }
        }
        if (sortParams != null)
        {

            query = _sortService.ApplySort(query, sortParams);
        }
        return query;
    }
}
