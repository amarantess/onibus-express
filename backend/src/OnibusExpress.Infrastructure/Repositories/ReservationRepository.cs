using Microsoft.EntityFrameworkCore;

using OnibusExpress.Domain.Entities;
using OnibusExpress.Domain.Enums;
using OnibusExpress.Domain.Features.Reservations.Shared;
using OnibusExpress.Domain.Repositories;
using OnibusExpress.Infrastructure.DataAccess;

namespace OnibusExpress.Infrastructure.Repositories;

public sealed class ReservationRepository : IReservationRepository
{
    private readonly OnibusExpressDbContext _dbContext;

    public ReservationRepository(OnibusExpressDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Reservation?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Reservations
            .Where(reservation => reservation.Code == code)
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<ReservationDetailsDto?> GetDetailsByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Reservations
            .Where(reservation => reservation.Code == code)
            .Select(reservation => new ReservationDetailsDto
            {
                Code = reservation.Code,
                Status = reservation.Status,
                SeatNumber = reservation.SeatNumber,
                CreatedAt = reservation.CreatedAt,
                CancelledAt = reservation.CancelledAt,
                IsDeleted = reservation.IsDeleted,
                PassengerId = reservation.PassengerId,
                PassengerFullName = reservation.Passenger!.FullName,
                PassengerCpf = reservation.Passenger.Cpf,
                PassengerEmail = reservation.Passenger.Email,
                PassengerBirthDate = reservation.Passenger.BirthDate,
                TripId = reservation.TripId,
                RouteId = reservation.Trip!.RouteId,
                Origin = reservation.Trip.Route!.Origin,
                Destination = reservation.Trip.Route.Destination,
                DepartureAt = reservation.Trip.DepartureAt,
                BasePrice = reservation.Trip.BasePrice
            })
            .SingleOrDefaultAsync(cancellationToken);
    }

    public Task<bool> ReservationCodeExistsAsync(string code, CancellationToken cancellationToken = default)
    {
        return _dbContext.Reservations.AnyAsync(reservation => reservation.Code == code, cancellationToken);
    }

    public Task<bool> SeatIsOccupiedAsync(Guid tripId, int seatNumber, CancellationToken cancellationToken = default)
    {
        return _dbContext.Reservations.AnyAsync(
            reservation => reservation.TripId == tripId
                && reservation.SeatNumber == seatNumber
                && !reservation.IsDeleted
                && reservation.Status != ReservationStatus.Cancelled,
            cancellationToken);
    }

    public async Task CreateAsync(Reservation reservation, CancellationToken cancellationToken = default)
    {
        await _dbContext.Reservations.AddAsync(reservation, cancellationToken);
    }

    public void Update(Reservation reservation)
    {
        _dbContext.Reservations.Update(reservation);
    }
}
