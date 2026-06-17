using System.Net;

namespace OnibusExpress.Infrastructure.Exceptions.ExceptionsBase;

public sealed class ConflictException : OnibusExpressException
{
    public ConflictException(string message) : base(message)
    {
    }

    public override IList<string> GetErrorMessages() => [Message];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Conflict;
}
