namespace OnibusExpress.Application.Extensions;

public static class StringExtensions
{
    public static string NormalizeCpf(this string cpf)
    {
        return new string(cpf.Where(char.IsDigit).ToArray());
    }
}
