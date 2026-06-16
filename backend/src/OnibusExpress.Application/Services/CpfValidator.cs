using System.Text.RegularExpressions;

using OnibusExpress.Application.Abstractions;
using OnibusExpress.Application.Extensions;

namespace OnibusExpress.Application.Services;

public sealed partial class CpfValidator : ICpfValidator
{
    public bool IsValid(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
        {
            return false;
        }

        var normalizedCpf = cpf.NormalizeCpf();

        if (normalizedCpf.Length != 11 || normalizedCpf.Distinct().Count() == 1)
        {
            return false;
        }

        return HasValidDigit(normalizedCpf, 9) && HasValidDigit(normalizedCpf, 10);
    }

    private static bool HasValidDigit(string cpf, int length)
    {
        var sum = 0;

        for (var index = 0; index < length; index++)
        {
            sum += (cpf[index] - '0') * ((length + 1) - index);
        }

        var remainder = (sum * 10) % 11;
        if (remainder == 10)
        {
            remainder = 0;
        }

        return remainder == (cpf[length] - '0');
    }
}
