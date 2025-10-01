using System.Linq.Expressions;
using Utils.Models;
using static MassTransit.Logging.LogCategoryName;
namespace Utils.FilterHelper;
public class PaginationParam
{
    public string? OrderBy { get; set; }
    public bool OrderDescending { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
public static class FilterTools<T>
{

    public static (IQueryable<T>, int TotalCount) ApplySortAndPagination(IQueryable<T> query, PaginationParam paginationParam)
    {
        int count = query.Count();
        if (!string.IsNullOrEmpty(paginationParam.OrderBy)) query = ApplySorting(query, paginationParam.OrderBy, paginationParam.OrderDescending);

        return (query.Skip((paginationParam.PageNumber - 1) * paginationParam.PageSize).Take(paginationParam.PageSize), count);
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
    private static (IQueryable<T>, int) Paging(IQueryable<T> query, PaginationParam paginationParam)
    {
        return FilterTools<T>.ApplySortAndPagination(query, paginationParam);
    }

} 
public class PagedResultT<T>
    {
        public List<PagedItem<T>> Items { get; set; } = [];
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }

        public PagedResultT(List<T> items, int totalCount, int pageNumber, int pageSize)
        {
            Items = Convertor(items, pageNumber, pageSize);
            TotalCount = totalCount;
            PageNumber = pageNumber;
            PageSize = pageSize;
            HasNextPage = (pageNumber * pageSize) < totalCount;
            HasPreviousPage = pageNumber >= 1;
        }
        public static List<PagedItem<T>> Convertor(List<T> items, int Number, int Size)
        {
            List<PagedItem<T>> list = [];
            for (int index = 0; index < items.Count; index++)
            {
                PagedItem<T> d = new(items[index], index + 1 + ((Number - 1) * Size));
                list.Add(d);
            }
            return list;
        }
    }