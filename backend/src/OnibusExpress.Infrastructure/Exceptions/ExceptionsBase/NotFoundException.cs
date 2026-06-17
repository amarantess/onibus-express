using System.Net;

namespace OnibusExpress.Infrastructure.Exceptions.ExceptionsBase;

public sealed class NotFoundException : OnibusExpressException
{
    public NotFoundException(string message) : base(message)
    {
    }

    public override IList<string> GetErrorMessages() => [Message];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.NotFound;
}
