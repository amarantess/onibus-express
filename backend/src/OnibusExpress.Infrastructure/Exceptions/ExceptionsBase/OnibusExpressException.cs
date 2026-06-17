using System.Net;

namespace OnibusExpress.Infrastructure.Exceptions.ExceptionsBase;

public abstract class OnibusExpressException : SystemException
{
    protected OnibusExpressException(string message) : base(message)
    {
    }

    public abstract IList<string> GetErrorMessages();
    public abstract HttpStatusCode GetStatusCode();
}
