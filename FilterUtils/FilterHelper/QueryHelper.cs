using System.Linq.Expressions;
using Utils.FilterModels;
namespace Utils.FilterHelper;
public static class QueryHelper
{
    public static PagedResult<TDto> ApplyQuery<T, TDto>(
        IQueryable<T> query, QueryParameters<T> parameters, Expression<Func<T, TDto>> selector)
    {
        if (parameters.Filter != null)
        {
            query = query.Where(parameters.Filter);
        }

        if (parameters.OrderBy != null)
        {
            query = parameters.OrderDescending
                ? query.OrderByDescending(parameters.OrderBy)
                : query.OrderBy(parameters.OrderBy);
        }

        int totalCount = query.Count();

        var pagedItems = query
            .Skip((parameters.PageNumber - 1) * parameters.PageSize)
            .Take(parameters.PageSize)
            .Select(selector) // 🔹 اینجا داده‌ها را به DTO تبدیل می‌کنیم
            .ToList();

        return new PagedResult<TDto>
        {
            Items = pagedItems,
            TotalCount = totalCount,
            PageNumber = parameters.PageNumber,
            PageSize = parameters.PageSize
        };
    }

}
//var queryParams = new QueryParameters<User>
//{
//    Filter = u => u.Age > 25, // کاربران بالای ۲۵ سال
//    OrderBy = u => u.Name, // مرتب‌سازی بر اساس نام
//    OrderDescending = false,
//    PageNumber = 1,
//    PageSize = 5
//};

//    var pagedUsers = ApplyQuery(_context.Users, queryParams, u => new UserDto
//    {
//        Name = u.Name,
//        Age = u.Age
//    });

//// نمایش خروجی
//Console.WriteLine($"Total Users: {pagedUsers.TotalCount}");
//foreach (var user in pagedUsers.Items)
//{
//    Console.WriteLine($"Name: {user.Name}, Age: {user.Age}");
//}
