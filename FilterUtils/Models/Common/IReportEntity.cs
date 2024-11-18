namespace Utils.Common;
public  interface IReportEntity
{
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreateOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public string? CreateBy { get; set; }
    public string? ModifiedBy { get; set; }
}
