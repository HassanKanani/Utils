
namespace FilterUtils.FilterModels.Sort;

public class SortParams
{
    public List<SortItem> SortItems { get; set; }
    public int? Take { get; set; }
    public int? Skip { get; set; } 
    public SortParams()
    {
        SortItems = new();
    
    }
}
