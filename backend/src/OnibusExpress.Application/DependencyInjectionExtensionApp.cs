using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

using OnibusExpress.Application.Abstractions;
using OnibusExpress.Application.Features.Reservations.CancelReservation;
using OnibusExpress.Application.Features.Reservations.CreateReservation;
using OnibusExpress.Application.Features.Reservations.GetReservationByCode;
using OnibusExpress.Application.Features.Routes.ListRoutes;
using OnibusExpress.Application.Features.Trips.GetTripDetails;
using OnibusExpress.Application.Features.Trips.SearchTrips;
using OnibusExpress.Application.Services;
using OnibusExpress.Domain.Features.Reservations.CreateReservation;
using OnibusExpress.Domain.Features.Routes.ListRoutes;
using OnibusExpress.Domain.Features.Trips.SearchTrips;

namespace OnibusExpress.Application;

public static class DependencyInjectionExtensionApp
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddUseCases();
        services.AddServices();
        services.AddValidators();

        return services;
    }

    private static void AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<ICreateReservationUseCase, CreateReservationUseCase>();
        services.AddScoped<IGetReservationByCodeUseCase, GetReservationByCodeUseCase>();
        services.AddScoped<ICancelReservationUseCase, CancelReservationUseCase>();
        services.AddScoped<IListRoutesUseCase, ListRoutesUseCase>();
        services.AddScoped<ISearchTripsUseCase, SearchTripsUseCase>();
        services.AddScoped<IGetTripDetailsUseCase, GetTripDetailsUseCase>();
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICpfValidator, CpfValidator>();
        services.AddScoped<IReservationCodeGenerator, ReservationCodeGenerator>();
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();
    }

    private static void AddValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreateReservationRequest>, CreateReservationRequestValidator>();
        services.AddScoped<IValidator<ListRoutesRequest>, ListRoutesRequestValidator>();
        services.AddScoped<IValidator<SearchTripsRequest>, SearchTripsRequestValidator>();
    }
}
