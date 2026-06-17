using System.Net;

namespace OnibusExpress.Infrastructure.Exceptions.ExceptionsBase;

public sealed class ErrorOnValidationException : OnibusExpressException
{
    private readonly IList<string> _errorMessages;

    public ErrorOnValidationException(IList<string> errorMessages) : base(string.Empty)
    {
        _errorMessages = errorMessages;
    }

    public ErrorOnValidationException(string errorMessage) : base(string.Empty)
    {
        _errorMessages = [errorMessage];
    }

    public override IList<string> GetErrorMessages() => _errorMessages;

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
}
