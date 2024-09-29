using FilterUtils.FilterModels.Sort;
namespace FilterUtils.FilterModels;
public class Filter
{
    public DynamicFilter? DynamicFilterParams { get; set; }
    public SortParams? SortParams { get; set; }
    public int PageSize { get; set; }
    public int Page { get; set; }


}
