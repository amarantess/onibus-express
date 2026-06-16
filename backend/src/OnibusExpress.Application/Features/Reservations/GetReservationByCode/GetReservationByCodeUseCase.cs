using OnibusExpress.Domain.Features.Reservations.GetReservationByCode;
using OnibusExpress.Domain.Repositories;

namespace OnibusExpress.Application.Features.Reservations.GetReservationByCode;

public sealed class GetReservationByCodeUseCase : IGetReservationByCodeUseCase
{
    private readonly IReservationRepository _reservationRepository;

    public GetReservationByCodeUseCase(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }

    public async Task<GetReservationByCodeResponse> ExecuteAsync(string code, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(code) || code.Length > 16)
			throw new Exception("Reservation code is invalid.");

		var reservation = await _reservationRepository.GetDetailsByCodeAsync(code, cancellationToken);
        if (reservation is null)
			throw new Exception("Reservation was not found.");

		return new GetReservationByCodeResponse
        {
            Code = reservation.Code,
            Status = reservation.Status,
            SeatNumber = reservation.SeatNumber,
            CreatedAt = reservation.CreatedAt,
            CancelledAt = reservation.CancelledAt,
            Passenger = new PassengerReservationResponse
            {
                PassengerId = reservation.PassengerId,
                FullName = reservation.PassengerFullName,
                Cpf = reservation.PassengerCpf,
                Email = reservation.PassengerEmail,
                BirthDate = reservation.PassengerBirthDate
            },
            Trip = new TripReservationResponse
            {
                TripId = reservation.TripId,
                RouteId = reservation.RouteId,
                Origin = reservation.Origin,
                Destination = reservation.Destination,
                DepartureAt = reservation.DepartureAt,
                BasePrice = reservation.BasePrice
            }
        };
    }
}
