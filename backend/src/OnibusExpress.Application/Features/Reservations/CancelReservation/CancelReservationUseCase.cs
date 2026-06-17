using OnibusExpress.Application.Abstractions;
using OnibusExpress.Domain.Enums;
using OnibusExpress.Domain.Features.Reservations.CancelReservation;
using OnibusExpress.Domain.Repositories;
using OnibusExpress.Infrastructure.Exceptions.ExceptionsBase;

namespace OnibusExpress.Application.Features.Reservations.CancelReservation;

public sealed class CancelReservationUseCase : ICancelReservationUseCase
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IReservationRepository _reservationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CancelReservationUseCase(
        IDateTimeProvider dateTimeProvider,
        IReservationRepository reservationRepository,
        IUnitOfWork unitOfWork)
    {
        _dateTimeProvider = dateTimeProvider;
        _reservationRepository = reservationRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CancelReservationResponse> ExecuteAsync(string code, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ErrorOnValidationException("Reservation code is required.");

        if (code.Length > 16)
            throw new ErrorOnValidationException("Reservation code must have at most 16 characters.");

        var reservation = await _reservationRepository.GetByCodeAsync(code, cancellationToken);
        if (reservation is null)
			throw new NotFoundException("Reservation was not found.");

		if (reservation.Status == ReservationStatus.Cancelled || reservation.IsDeleted)
			throw new ConflictException("Reservation is already cancelled.");

		var reservationDetails = await _reservationRepository.GetDetailsByCodeAsync(code, cancellationToken);
        if (reservationDetails is null)
			throw new NotFoundException("Reservation was not found.");

		if (reservationDetails.DepartureAt - _dateTimeProvider.UtcNow < TimeSpan.FromHours(2))
			throw new ConflictException("Cancellation is only allowed up to 2 hours before departure.");

		var cancelledAt = _dateTimeProvider.UtcNow;

        reservation.Status = ReservationStatus.Cancelled;
        reservation.CancelledAt = cancelledAt;
        reservation.IsDeleted = true;
        reservation.DeletedAt = cancelledAt;
        reservation.UpdatedAt = cancelledAt;

        _reservationRepository.Update(reservation);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new CancelReservationResponse
        {
            Code = reservation.Code,
            Status = reservation.Status,
            CancelledAt = reservation.CancelledAt ?? cancelledAt
        };
    }
}
