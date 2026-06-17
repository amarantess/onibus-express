namespace OnibusExpress.Api.Responses;

public sealed class ResponseError
{
    public IList<string> Errors { get; set; }

    public ResponseError(IList<string> errors)
    {
        Errors = errors;
    }

    public ResponseError(string error)
    {
        Errors = [error];
    }
}
