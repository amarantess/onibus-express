using OnibusExpress.Application.Abstractions;
using OnibusExpress.Application.Features.Reservations.CancelReservation;
using OnibusExpress.Application.Features.Reservations.CreateReservation;
using OnibusExpress.Application.Services;
using OnibusExpress.Infrastructure.DataAccess;
using OnibusExpress.Infrastructure.Repositories;
using OnibusExpress.Tests.TestUtilities.Fakes;

namespace OnibusExpress.Tests.TestUtilities.UseCases;

internal static class ReservationUseCaseFactory
{
    public static CreateReservationUseCase CreateReservationUseCase(
        OnibusExpressDbContext dbContext,
        DateTimeOffset utcNow,
        IReservationCodeGenerator? reservationCodeGenerator = null)
    {
        return new CreateReservationUseCase(
            new CreateReservationRequestValidator(),
            new CpfValidator(),
            reservationCodeGenerator ?? new SequentialReservationCodeGenerator("ABC-12345"),
            new FakeDateTimeProvider(utcNow),
            new TripRepository(dbContext),
            new PassengerRepository(dbContext),
            new ReservationRepository(dbContext),
            new UnitOfWork(dbContext));
    }

    public static CancelReservationUseCase CancelReservationUseCase(OnibusExpressDbContext dbContext, DateTimeOffset utcNow)
    {
        return new CancelReservationUseCase(
            new FakeDateTimeProvider(utcNow),
            new ReservationRepository(dbContext),
            new UnitOfWork(dbContext));
    }
}
