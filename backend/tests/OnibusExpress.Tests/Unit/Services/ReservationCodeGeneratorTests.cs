using System.Text.RegularExpressions;

using FluentAssertions;

using OnibusExpress.Application.Services;

namespace OnibusExpress.Tests.Unit.Services;

public sealed partial class ReservationCodeGeneratorTests
{
    private readonly ReservationCodeGenerator _generator = new();

    [Fact]
    public void Generate_ShouldReturnReadableCode()
    {
        var code = _generator.Generate();

        code.Should().NotBeNullOrWhiteSpace();
        ReservationCodeRegex().IsMatch(code).Should().BeTrue();
    }

    [GeneratedRegex("^[A-Z]{3}-[0-9]{5}$")]
    private static partial Regex ReservationCodeRegex();
}
