
namespace Utils.Models;
public class PagedItem<T>
{
    public T Data { get; set; }
    public int RecordNumber { get; set; }

    public PagedItem(T data, int recordNumber)
    {
        Data = data;
        RecordNumber = recordNumber;
    }
}
