namespace Utils.Models;
using System.Collections.Generic;
public class QueryParametersInputDto
{
    public List<FilterCriteriaDto> Filters { get; set; } = new List<FilterCriteriaDto>();
    public string OrderBy { get; set; }
    public bool OrderDescending { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

