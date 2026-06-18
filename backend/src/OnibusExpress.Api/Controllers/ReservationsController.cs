using Microsoft.AspNetCore.Mvc;
using OnibusExpress.Api.Responses;
using OnibusExpress.Application.Features.Reservations.CancelReservation;
using OnibusExpress.Application.Features.Reservations.CreateReservation;
using OnibusExpress.Application.Features.Reservations.GetReservationByCode;
using OnibusExpress.Domain.Features.Reservations.CancelReservation;
using OnibusExpress.Domain.Features.Reservations.CreateReservation;
using OnibusExpress.Domain.Features.Reservations.GetReservationByCode;

namespace OnibusExpress.Api.Controllers;

[ApiController]
[Route("reservas")]
public class ReservationsController : ControllerBase
{
    private readonly ICreateReservationUseCase _createReservationUseCase;
    private readonly IGetReservationByCodeUseCase _getReservationByCodeUseCase;
    private readonly ICancelReservationUseCase _cancelReservationUseCase;

    public ReservationsController(
        ICreateReservationUseCase createReservationUseCase,
        IGetReservationByCodeUseCase getReservationByCodeUseCase,
        ICancelReservationUseCase cancelReservationUseCase)
    {
        _createReservationUseCase = createReservationUseCase;
        _getReservationByCodeUseCase = getReservationByCodeUseCase;
        _cancelReservationUseCase = cancelReservationUseCase;
    }

    [HttpPost]
    [ProducesResponseType<CreateReservationResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType<ResponseError>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ResponseError>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ResponseError>(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateReservation([FromBody] CreateReservationRequest request, CancellationToken cancellationToken)
    {
        var response = await _createReservationUseCase.ExecuteAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetReservationByCode), new { code = response.Code }, response);
    }

    [HttpGet("{code}")]
    [ProducesResponseType<GetReservationByCodeResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ResponseError>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ResponseError>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetReservationByCode([FromRoute] string code, CancellationToken cancellationToken)
    {
        var response = await _getReservationByCodeUseCase.ExecuteAsync(code, cancellationToken);
        return Ok(response);
    }

    [HttpDelete("{code}")]
    [ProducesResponseType<CancelReservationResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ResponseError>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ResponseError>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ResponseError>(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CancelReservation([FromRoute] string code, CancellationToken cancellationToken)
    {
        var response = await _cancelReservationUseCase.ExecuteAsync(code, cancellationToken);
        return Ok(response);
    }
}
