
namespace Utils.Pagination;
public static class QueryableExtensions
{
    public static PagedResult<T> GetPaged<T>(this IQueryable<T> query, int page, int pageSize)
    {
        var result = new PagedResult<T>();

        result.CurrentPage = page;
        result.PageSize = pageSize;
        result.TotalCount = query.Count();
        result.Items = query.Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .ToList();
        
        return result;
    }
}
