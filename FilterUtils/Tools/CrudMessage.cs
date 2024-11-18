namespace Application.StaticVariable;
public static class CrudMessage
{
    public static string CreateSuccess(string entityName)
    {
        return $" {entityName} با موفقیت اضافه شد ";
    }
  
    public static string CreateFailure(string entityName)
    {
        return $"افزودن {entityName} با خطا مواجه شد";
    }
    public static string DeleteSuccess(string entityName)
    {
        return $"حذف {entityName} با موفقیت انجام شد ";
    } 
    public static string DeleteFailure(string entityName)
    {
        return $"حذف {entityName} با خطا مواجه شد";
    }
    public static string UpdateSuccess(string entityName)
    {
        return $"ویرایش {entityName} با موفقیت انجام شد ";
    } 
    public static string UpdateFailure(string entityName)
    {
        return $"ویرایش {entityName} با خطا مواجه شد";
    }
    public static string NullIDFailure(string entityName)
    {
        return $"لطفا {entityName} مورد نظر را وارد کنید";
    }
    public static string NotExistFailure(string entityName)
    {
        return $" {entityName} مورد نظر وجود ندارد";
    }
    public static string ConfirmSuccess(string entityName)
    {
        return $" {entityName} مورد نظر با موفقیت تایید شد";
    }
    public static string NotVerified(string entityName)
    {
        return $" {entityName} مورد نظر  تایید شده است و امکان ویرایش وجود ندارد";
    }
    public static string NotVerifiedDel(string entityName)
    {
        return $" {entityName} مورد نظر  تایید شده است و امکان حذف وجود ندارد";
    }
    public static string NotActive(string entityName)
    {
        return $" {entityName} مورد نظر غیرفعال است";
    }
    public static string IsReserved(string entityName)
    {
        return $" {entityName} مورد نظر قبلا رزرو شده است";
    }
    public static string IsDeleted(string entityName)
    {
        return $" {entityName} مورد نظر قبلا حذف شده است";
    }
    public static string BlankFile (string entityName)
    {
        return $" {entityName} بارگزاری نشده است";
    }
    public static string NullList(string entityName)
    {
        return $" لیست {entityName} خالی است";
    }
    public static string repetitive(string entityName)
    {
        return $" {entityName} قبلا ایجاد شده است";
    }
    public static string DeleteErroForRelationship(string entityName)
    {
        return $" لطفا ابتدا {entityName} مربوط به آن را حذف کنید";
    }
    public static string CreateSuccessAutoDesignHotel(string entityName)
    {
        return $" چیدمان {entityName} با موفقیت انجام شد ";
    }
    public static string Status(string entityName)
    {
        return $"وضعیت به {entityName} تغییر کرد ";
    }
}
