

namespace Utils.Models;

public class FilterCriteriaDto
{
    public string PropertyName { get; set; }
    public FilterOperator Operator { get; set; }
    public string Value { get; set; }
}
