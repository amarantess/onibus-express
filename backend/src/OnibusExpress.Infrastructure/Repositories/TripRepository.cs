using Microsoft.EntityFrameworkCore;

using OnibusExpress.Domain.Entities;
using OnibusExpress.Domain.Features.Trips.GetTripDetails;
using OnibusExpress.Domain.Features.Trips.SearchTrips;
using OnibusExpress.Domain.Repositories;
using OnibusExpress.Infrastructure.DataAccess;

namespace OnibusExpress.Infrastructure.Repositories;

public sealed class TripRepository : ITripRepository
{
    private readonly OnibusExpressDbContext _dbContext;

    public TripRepository(OnibusExpressDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<TripSummaryResponse>> SearchAsync(SearchTripsRequest request, CancellationToken cancellationToken = default)
    {
        var start = new DateTimeOffset(request.TravelDate.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc), TimeSpan.Zero);
        var end = new DateTimeOffset(request.TravelDate.AddDays(1).ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc), TimeSpan.Zero);

        return await _dbContext.Trips
            .Where(trip => !trip.IsDeleted
                && trip.Route != null
                && !trip.Route.IsDeleted
                && trip.Route.Origin == request.Origin
                && trip.Route.Destination == request.Destination
                && trip.DepartureAt >= start
                && trip.DepartureAt < end)
            .OrderBy(trip => trip.DepartureAt)
            .Select(trip => new TripSummaryResponse
            {
                TripId = trip.Id,
                RouteId = trip.RouteId,
                Origin = trip.Route!.Origin,
                Destination = trip.Route.Destination,
                DepartureAt = trip.DepartureAt,
                BasePrice = trip.BasePrice,
                AvailableSeats = trip.AvailableSeats,
                RemainingSeats = trip.AvailableSeats - trip.Reservations.Count(reservation => !reservation.IsDeleted)
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<TripDetailsResponse?> GetDetailsAsync(Guid tripId, CancellationToken cancellationToken = default)
    {
        var trip = await _dbContext.Trips
            .Where(item => item.Id == tripId && !item.IsDeleted && item.Route != null && !item.Route.IsDeleted)
            .Select(item => new
            {
                item.Id,
                item.RouteId,
                item.DepartureAt,
                item.BasePrice,
                item.AvailableSeats,
                Origin = item.Route!.Origin,
                Destination = item.Route.Destination,
                EstimatedDuration = item.Route.EstimatedDuration,
                OccupiedSeats = item.Reservations
                    .Where(reservation => !reservation.IsDeleted)
                    .Select(reservation => reservation.SeatNumber)
                    .ToList()
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (trip is null)
        {
            return null;
        }

        var seats = Enumerable.Range(1, trip.AvailableSeats)
            .Select(seatNumber => new SeatAvailabilityResponse
            {
                SeatNumber = seatNumber,
                IsOccupied = trip.OccupiedSeats.Contains(seatNumber)
            })
            .ToArray();

        return new TripDetailsResponse
        {
            TripId = trip.Id,
            RouteId = trip.RouteId,
            Origin = trip.Origin,
            Destination = trip.Destination,
            EstimatedDuration = trip.EstimatedDuration,
            DepartureAt = trip.DepartureAt,
            BasePrice = trip.BasePrice,
            AvailableSeats = trip.AvailableSeats,
            Seats = seats
        };
    }

    public async Task<Trip?> GetByIdAsync(Guid tripId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Trips
            .Include(trip => trip.Route)
            .SingleOrDefaultAsync(trip => trip.Id == tripId && !trip.IsDeleted, cancellationToken);
    }
}
