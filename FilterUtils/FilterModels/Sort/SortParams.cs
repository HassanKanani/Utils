
namespace FilterUtils.FilterModels.Sort;

public class SortParams
{
    public List<SortItem> SortItems { get; set; }
    public SortParams()
    {
        SortItems = new();
    
    }
}
