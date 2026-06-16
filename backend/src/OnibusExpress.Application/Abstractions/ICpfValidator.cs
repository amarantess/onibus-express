namespace OnibusExpress.Application.Abstractions;

public interface ICpfValidator
{
    bool IsValid(string cpf);
}
