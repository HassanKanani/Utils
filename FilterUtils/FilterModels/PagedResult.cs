namespace Utils.FilterModels;
public class PagedResult<T>
{
    public List<T> Items { get; set; } = new List<T>();  // داده‌های صفحه
    public int TotalCount { get; set; } // تعداد کل آیتم‌ها
    public int PageNumber { get; set; } // شماره صفحه فعلی
    public int PageSize { get; set; } // تعداد در هر صفحه
    public bool HasNext => PageNumber * PageSize < TotalCount; // آیا صفحه بعدی وجود دارد؟
    public bool HasPrevious => PageNumber > 1; // آیا صفحه قبلی وجود دارد؟
}