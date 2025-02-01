using System.Linq.Expressions;

namespace Utils.FilterModels;
public class QueryParameters<T>
{
    public Expression<Func<T, bool>> Filter { get; set; } = x => true; // برای سازگاری با EF Core
    public Expression<Func<T, object>> OrderBy { get; set; } = null;
    public bool OrderDescending { get; set; } = false;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
