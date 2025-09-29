using Utils.Models;
namespace Application.StaticVariable;
public static class CrudMessage
{
    public static ApiResponse<bool> CreateSuccess(string entityName)
    {
        return ApiResponse<bool>.CreateSuccessResponse(true, $" {entityName} با موفقیت اضافه شد ");
    }
    public static ApiResponse<bool> SuccessSpecialMessage(string entityName)
    {
        return ApiResponse<bool>.CreateSuccessResponse(true, entityName);
    }
    public static ApiResponse<bool> FailureSpecialMessage(string entityName)
    {
        return ApiResponse<bool>.CreateErrorResponse(false, entityName);
    }
    public static ApiResponse<bool> CreateFailure(string entityName)
    {
        return ApiResponse<bool>.CreateErrorResponse(false, $"افزودن {entityName} با خطا مواجه شد");
    }
    public static ApiResponse<bool> DeleteSuccess(string entityName)
    {
        return ApiResponse<bool>.CreateSuccessResponse(true, $"حذف {entityName} با موفقیت انجام شد ");
    }
    public static ApiResponse<bool> DeleteFailure(string entityName)
    {
        return ApiResponse<bool>.CreateErrorResponse(false, $"حذف {entityName} با خطا مواجه شد");
    }
    public static ApiResponse<bool> UpdateSuccess(string entityName)
    {
        return ApiResponse<bool>.CreateSuccessResponse(true, $"ویرایش {entityName} با موفقیت انجام شد ");
    }
    public static ApiResponse<bool> UpdateFailure(string entityName)
    {
        return ApiResponse<bool>.CreateErrorResponse(false, $"ویرایش {entityName} با خطا مواجه شد");
    }
    public static string NullIDFailure(string entityName)
    {
        return $"لطفا {entityName} مورد نظر را وارد کنید";
    }
    public static ApiResponse<bool> NullIDFailureCheck(string entityName)
    {
        return ApiResponse<bool>.CreateErrorResponse(false, $"لطفا {entityName} مورد نظر را وارد کنید");
    }
    public static string NotExistFailure(string entityName)
    {
        return $" {entityName} مورد نظر وجود ندارد";
    }
    public static ApiResponse<bool> NotExistFailureCheck(string entityName)
    {
        return ApiResponse<bool>.CreateErrorResponse(false, $" {entityName} مورد نظر وجود ندارد");
    }
    public static ApiResponse<bool> ConfirmSuccess(string entityName)
    {
        return ApiResponse<bool>.CreateSuccessResponse(true, $" {entityName} مورد نظر با موفقیت تایید شد");
    }
    public static string NotVerified(string entityName)
    {
        return $" {entityName} مورد نظر  تایید شده است و امکان ویرایش وجود ندارد";
    }
    public static ApiResponse<bool> NotVerifiedCheck(string entityName)
    {
        return ApiResponse<bool>.CreateErrorResponse(false, $" {entityName} مورد نظر  تایید شده است و امکان ویرایش وجود ندارد");
    }
    public static string NotVerifiedDel(string entityName)
    {
        return $" {entityName} مورد نظر  تایید شده است و امکان حذف وجود ندارد";
    }
    public static ApiResponse<bool> NotVerifiedDelChech(string entityName)
    {
        return ApiResponse<bool>.CreateErrorResponse(false, $" {entityName} مورد نظر  تایید شده است و امکان حذف وجود ندارد");
    }
    public static string NotActive(string entityName)
    {
        return $" {entityName} مورد نظر غیرفعال است";
    }
    public static ApiResponse<bool> NotActiveCheck(string entityName)
    {
        return ApiResponse<bool>.CreateErrorResponse(false, $" {entityName} مورد نظر غیرفعال است");
    }
    public static string IsReserved(string entityName)
    {
        return $" {entityName} مورد نظر قبلا رزرو شده است";
    }
    public static ApiResponse<bool> IsReservedCheck(string entityName)
    {
        return ApiResponse<bool>.CreateErrorResponse(false, $" {entityName} مورد نظر قبلا رزرو شده است");
    }
    public static string IsDeleted(string entityName)
    {
        return $" {entityName} مورد نظر قبلا حذف شده است";
    }
    public static ApiResponse<bool> IsDeletedCheck(string entityName)
    {
        return ApiResponse<bool>.CreateErrorResponse(false, $" {entityName} مورد نظر قبلا حذف شده است");
    }
    public static string BlankFile(string entityName)
    {
        return $" {entityName} بارگذاری نشده است";
    }
    public static ApiResponse<bool> BlankFileCheck(string entityName)
    {
        return ApiResponse<bool>.CreateErrorResponse(false, $" {entityName} بارگذاری نشده است");
    }
    public static string NullList(string entityName)
    {
        return $" لیست {entityName} خالی است";
    }
    public static ApiResponse<bool> NullListCheck(string entityName)
    {
        return ApiResponse<bool>.CreateErrorResponse(false, $" لیست {entityName} خالی است");
    }
    public static ApiResponse<bool> repetitive(string entityName)
    {
        return ApiResponse<bool>.CreateErrorResponse(false, $" {entityName} قبلا ایجاد شده است");
    }
    public static string DeleteErroForRelationship(string entityName)
    {
        return $" لطفا ابتدا {entityName} مربوط به آن را حذف کنید";
    }
    public static ApiResponse<bool> DeleteErroForRelationshipCheck(string entityName)
    {
        return ApiResponse<bool>.CreateErrorResponse(false, $" لطفا ابتدا {entityName} مربوط به آن را حذف کنید");
    }
    public static string CreateSuccessAutoDesignHotel(string entityName)
    {
        return $" چیدمان {entityName} با موفقیت انجام شد ";
    }
    public static ApiResponse<bool> CreateSuccessAutoDesignHotelCheck(string entityName)
    {
        return ApiResponse<bool>.CreateSuccessResponse(true, $" چیدمان {entityName} با موفقیت انجام شد ");
    }
    public static ApiResponse<bool> Status(string entityName)
    {
        return ApiResponse<bool>.CreateSuccessResponse(true, $"وضعیت به {entityName} تغییر کرد ");
    }
    public static ApiResponse<bool> NotEditable(string entityName)
    {
        return ApiResponse<bool>.CreateErrorResponse(false, $"{entityName} قابل ویرایش نیست  ");
    }
    public static string NotAccess(string entityName)
    {
        return $"دسترسی شما به {entityName} محدود است";
    }
    public static ApiResponse<bool> NotAccessCheck(string entityName)
    {
        return ApiResponse<bool>.CreateErrorResponse(false, $"دسترسی شما به {entityName} محدود است");
    }
}
