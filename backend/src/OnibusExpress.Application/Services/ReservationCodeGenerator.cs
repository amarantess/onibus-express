using System.Security.Cryptography;

using OnibusExpress.Application.Abstractions;

namespace OnibusExpress.Application.Services;

public sealed class ReservationCodeGenerator : IReservationCodeGenerator
{
    private const string Letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    public string Generate()
    {
        Span<char> code = stackalloc char[9];

        for (var index = 0; index < 3; index++)
        {
            code[index] = Letters[RandomNumberGenerator.GetInt32(Letters.Length)];
        }

        code[3] = '-';

        for (var index = 4; index < code.Length; index++)
        {
            code[index] = (char)('0' + RandomNumberGenerator.GetInt32(10));
        }

        return new string(code);
    }
}
