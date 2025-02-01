
using FilterUtils.FilterModels.Sort;

namespace Utils.Models.Common;

public class FilterParam
{
    public SortItem SortParam { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}
