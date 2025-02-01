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

    //public Task<PagedResult<UserResponse>> DynamicFilter()
    //{
    //    var queryParams = new QueryParameters<User>
    //    {
    //        Filter = u => u.UseName == "string",
    //        OrderBy = u => u.Name,
    //        OrderDescending = false,
    //        PageNumber = 1,
    //        PageSize = 10
    //    };
    //    var pagedUsers = QueryHelper.ApplyQuery<User, UserResponse>(
    //_userRepository.TableNoTracking,
    //queryParams,
    //users => _mapper.Map<UserResponse>(users)
    //   );

    //    return Task.FromResult(pagedUsers);
    //}