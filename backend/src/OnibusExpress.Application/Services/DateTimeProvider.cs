using OnibusExpress.Application.Abstractions;

namespace OnibusExpress.Application.Services;

public sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
}
