namespace Utils.FilterHelper;
using System.Collections.Generic;
using Utils.Models;

public class PagedResult<T>
{
    public List<PagedItem<T>> Items { get; set; } = new List<PagedItem<T>>();
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public bool HasNextPage { get; set; }
    public bool HasPreviousPage { get; set; }

    public PagedResult(List<PagedItem<T>> items, int totalCount, int pageNumber, int pageSize)
    {
        Items = items;
        TotalCount = totalCount;
        PageNumber = pageNumber;
        PageSize = pageSize;
        HasNextPage = (pageNumber * pageSize) < totalCount;
        HasPreviousPage = pageNumber > 1;
    }
}
