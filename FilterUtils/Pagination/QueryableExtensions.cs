
namespace Utils.Pagination;
public  class QueryableExtensions<T>
{
    public  PagedResult<T> GetPaged( IQueryable<T> query, int page, int pageSize)
    {
        PagedResult<T> result = new ();

        result.CurrentPage = page;
        result.PageSize = pageSize;
        result.TotalCount = query.Count();
        result.Items = query.Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .ToList();
        
        return result;
    }
    public PagedResult<T> GetPaged(List<T> query, int page, int pageSize)
    {
        PagedResult<T> result = new();

        result.CurrentPage = page;
        result.PageSize = pageSize;
        result.TotalCount = query.Count();
        result.Items = query.Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .ToList();

        return result;
    }
}
