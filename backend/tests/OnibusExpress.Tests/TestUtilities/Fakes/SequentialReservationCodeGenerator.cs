using OnibusExpress.Application.Abstractions;

namespace OnibusExpress.Tests.TestUtilities.Fakes;

internal sealed class SequentialReservationCodeGenerator : IReservationCodeGenerator
{
    private readonly Queue<string> _codes;

    public SequentialReservationCodeGenerator(params string[] codes)
    {
        _codes = new Queue<string>(codes);
    }

    public string Generate()
    {
        return _codes.Count > 0 ? _codes.Dequeue() : "ZZZ-99999";
    }
}
