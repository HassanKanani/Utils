using FilterUtils.FilterModels.Sort;
using System.Linq.Expressions;
namespace FilterUtils.Services;

public class SortService
{
    public IQueryable<T> ApplySort<T>(IQueryable<T> query, SortParams sortParams)
    {
        // اعمال مرتب‌سازی بر اساس فیلدهای مختلف
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
                    orderedQuery = sortItem.IsDescending? query.OrderByDescending(lambda): query.OrderBy(lambda);
                }
                else
                {
                    orderedQuery = sortItem.IsDescending ? orderedQuery.ThenByDescending(lambda): orderedQuery.ThenBy(lambda);
                }
            }
        }

        // اگر هیچ مرتب‌سازی انجام نشده باشد، از کوئری اصلی استفاده کنید
        var result = orderedQuery ?? query;

        // اعمال skip و take
        return result.Skip(sortParams.Skip??0).Take(sortParams.Take??10);
    }
}