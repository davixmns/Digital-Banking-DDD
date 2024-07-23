namespace DigitalBankDDD.Application.Wrapper;

public class ApiResult<T>
{
    public T? Data { get; private set; }
    public bool IsSuccess { get; private set; }
    public string? ErrorMessage { get; private set; }

    private ApiResult(T? data, bool isSuccess, string? errorMessage)
    {
        Data = data;
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
    }

    public static ApiResult<T> Success(T data) => new ApiResult<T>(data, true, null);
    public static ApiResult<T> Failure(string errorMessage) => new ApiResult<T>(default, false, errorMessage);
}