namespace Utils.Models;
public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }
    public ApiResponse()
    {

    }
    public ApiResponse(bool Success, string Messgage)
    {
        this.Success = Success;
        this.Message = Messgage;

    }
    public ApiResponse(bool Success, string Messgage, T? Data)
    {
        this.Success = Success;
        this.Message = Messgage;
        this.Data = Data;
    }

    const string SuccessMessage = "عملیات موفق";
    const bool IsSuccess = true;
    public static ApiResponse<T> CreateSuccessResponse(T data, string? Message = null)
    {
        return new ApiResponse<T>(IsSuccess, Message ?? SuccessMessage, data);
    }
    public static ApiResponse<T> CreateSuccessResponse(string? Message = null)
    {
        return new ApiResponse<T>(IsSuccess, Message ?? SuccessMessage);
    }
    const string ErrorMessage = "عملیات شکست خورد";

    public static ApiResponse<T> CreateErrorResponse(T data, string? Message = null)
    {
        return new ApiResponse<T>(!IsSuccess, Message ?? ErrorMessage, data);
    }
    public static ApiResponse<T> CreateErrorResponse(string? Message = null)
    {
        return new ApiResponse<T>(!IsSuccess, Message ?? ErrorMessage);
    }
}