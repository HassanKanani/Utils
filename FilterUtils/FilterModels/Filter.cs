using FilterUtils.FilterModels.Sort;
namespace FilterUtils.FilterModels;
public class Filter
{
    public DynamicFilter? DynamicFilterParams { get; set; }
    public SortParams? SortParams { get; set; }
 
}
