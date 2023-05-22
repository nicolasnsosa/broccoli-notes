namespace Infrastructure.Functions;

public class FunctionResponse : FunctionResponse<object>
{
}

public class FunctionResponse<TData>
{
    public TData? Data { get; set; }
    
    public bool Status => !Errors.Any();
    public List<FunctionResponseError> Errors { get; }

    public FunctionResponse()
    {
        Errors = new List<FunctionResponseError>();
    }

    public void AddError(FunctionResponseError error)
    {
        Errors.Add(error);
    }

    public void AddError(string errorCode, string description)
    {
        Errors.Add(new FunctionResponseError(errorCode, description));
    }

    public void AddError(Exception ex)
    {
        Errors.Add(new FunctionResponseError(ex));
    }

    public void AddErrors(IEnumerable<FunctionResponseError> errors)
    {
        Errors.AddRange(errors);
    }

    public string GetUnifiedErrorMessage()
    {
        return string.Join(Environment.NewLine, Errors.Select(e => e.ErrorMessage));
    }
}

public class FunctionResponseError
{
    public string ErrorCode { get; }
    public string Description { get; }
    public string? StackTrace { get; }

    public string ErrorMessage => $"Error {ErrorCode}: {Description}";

    public FunctionResponseError(string errorCode, string description)
    {
        ErrorCode = errorCode;
        Description = description;
    }

    public FunctionResponseError(Exception ex)
    {
        ErrorCode = ex.HResult.ToString();
        Description = ex.Message;
        StackTrace = ex.StackTrace;
    }
}