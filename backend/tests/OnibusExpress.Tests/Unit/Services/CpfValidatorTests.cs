using FluentAssertions;

using OnibusExpress.Application.Services;

namespace OnibusExpress.Tests.Unit.Services;

public sealed class CpfValidatorTests
{
    private readonly CpfValidator _validator = new();

    [Fact]
    public void IsValid_ShouldReturnTrue_WhenCpfIsValid()
    {
        var result = _validator.IsValid("52998224725");

        result.Should().BeTrue();
    }

    [Fact]
    public void IsValid_ShouldReturnFalse_WhenCpfIsInvalid()
    {
        var result = _validator.IsValid("12345678900");

        result.Should().BeFalse();
    }

    [Fact]
    public void IsValid_ShouldReturnTrue_WhenCpfHasFormattingCharacters()
    {
        var result = _validator.IsValid("529.982.247-25");

        result.Should().BeTrue();
    }
}
