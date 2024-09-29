namespace FilterUtils.FilterModels;
public class FilterItem
{
    public string Name { get; set; }
    public string Value { get; set; }
    public ComparisonType Comparison { get; set; } = ComparisonType.Equals;
}
