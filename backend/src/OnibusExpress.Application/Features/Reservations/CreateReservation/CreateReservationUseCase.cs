using FluentValidation;

using OnibusExpress.Application.Abstractions;
using OnibusExpress.Application.Extensions;
using OnibusExpress.Domain.Entities;
using OnibusExpress.Domain.Enums;
using OnibusExpress.Domain.Features.Reservations.CreateReservation;
using OnibusExpress.Domain.Repositories;
using OnibusExpress.Infrastructure.Exceptions.ExceptionsBase;

namespace OnibusExpress.Application.Features.Reservations.CreateReservation;

public sealed class CreateReservationUseCase : ICreateReservationUseCase
{
    private readonly IValidator<CreateReservationRequest> _validator;
    private readonly ICpfValidator _cpfValidator;
    private readonly IReservationCodeGenerator _reservationCodeGenerator;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ITripRepository _tripRepository;
    private readonly IPassengerRepository _passengerRepository;
    private readonly IReservationRepository _reservationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateReservationUseCase(
        IValidator<CreateReservationRequest> validator,
        ICpfValidator cpfValidator,
        IReservationCodeGenerator reservationCodeGenerator,
        IDateTimeProvider dateTimeProvider,
        ITripRepository tripRepository,
        IPassengerRepository passengerRepository,
        IReservationRepository reservationRepository,
        IUnitOfWork unitOfWork)
    {
        _validator = validator;
        _cpfValidator = cpfValidator;
        _reservationCodeGenerator = reservationCodeGenerator;
        _dateTimeProvider = dateTimeProvider;
        _tripRepository = tripRepository;
        _passengerRepository = passengerRepository;
        _reservationRepository = reservationRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateReservationResponse> ExecuteAsync(CreateReservationRequest request, CancellationToken cancellationToken = default)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var normalizedCpf = request.Cpf.NormalizeCpf();

        if (!_cpfValidator.IsValid(normalizedCpf))
			throw new ErrorOnValidationException("CPF is invalid.");

		var trip = await _tripRepository.GetByIdAsync(request.TripId, cancellationToken);
        if (trip is null)
			throw new NotFoundException("Trip was not found.");

		if (trip.DepartureAt <= _dateTimeProvider.UtcNow)
			throw new ConflictException("Cannot create a reservation for a trip that has already departed.");

		if (await _reservationRepository.SeatIsOccupiedAsync(request.TripId, request.SeatNumber, cancellationToken))
			throw new ConflictException("The selected seat is already occupied.");

		var passenger = await _passengerRepository.GetByCpfAsync(normalizedCpf, cancellationToken);
        if (passenger is null)
        {
            passenger = new Passenger
            {
                Id = Guid.NewGuid(),
                FullName = request.FullName,
                Cpf = normalizedCpf,
                Email = request.Email,
                BirthDate = request.BirthDate,
                CreatedAt = _dateTimeProvider.UtcNow
            };

            await _passengerRepository.CreateAsync(passenger, cancellationToken);
        }
        else
        {
            passenger.FullName = request.FullName;
            passenger.Email = request.Email;
            passenger.BirthDate = request.BirthDate;
            passenger.UpdatedAt = _dateTimeProvider.UtcNow;

            _passengerRepository.Update(passenger);
        }

        var reservationCode = await GenerateUniqueCodeAsync(cancellationToken);
        var createdAt = _dateTimeProvider.UtcNow;

        var reservation = new Reservation
        {
            Id = Guid.NewGuid(),
            Code = reservationCode,
            TripId = trip.Id,
            PassengerId = passenger.Id,
            SeatNumber = request.SeatNumber,
            Status = ReservationStatus.Confirmed,
            CreatedAt = createdAt,
            IsDeleted = false
        };

        await _reservationRepository.CreateAsync(reservation, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new CreateReservationResponse
        {
            Code = reservation.Code,
            TripId = reservation.TripId,
            PassengerId = reservation.PassengerId,
            SeatNumber = reservation.SeatNumber,
            Status = reservation.Status,
            CreatedAt = reservation.CreatedAt
        };
    }

    private async Task<string> GenerateUniqueCodeAsync(CancellationToken cancellationToken)
    {
        const int maxAttempts = 10;

        for (var attempt = 0; attempt < maxAttempts; attempt++)
        {
            var code = _reservationCodeGenerator.Generate();
            if (!await _reservationRepository.ReservationCodeExistsAsync(code, cancellationToken))
            {
                return code;
            }
        }

        throw new Exception("Unable to generate a unique reservation code.");
    }
}
