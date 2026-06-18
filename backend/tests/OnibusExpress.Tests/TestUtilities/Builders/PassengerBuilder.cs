using OnibusExpress.Domain.Entities;

namespace OnibusExpress.Tests.TestUtilities.Builders;

internal static class PassengerBuilder
{
    public static Passenger Build(DateTimeOffset createdAt)
    {
        return new Passenger
        {
            Id = Guid.NewGuid(),
            FullName = "Maria Silva",
            Cpf = "52998224725",
            Email = "maria.silva@example.com",
            BirthDate = new DateOnly(1995, 6, 15),
            CreatedAt = createdAt
        };
    }
}
