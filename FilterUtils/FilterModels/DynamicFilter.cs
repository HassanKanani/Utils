namespace FilterUtils.FilterModels;
public   class DynamicFilter
{

    public List<FilterItem> FilterItems { get; set; }
    public DynamicFilter()
    {
        FilterItems = new ();
    }
}
