using Microsoft.AspNetCore.Mvc;
using OnibusExpress.Api.Responses;
using OnibusExpress.Application.Features.Routes.ListRoutes;
using OnibusExpress.Domain.Features.Routes.ListRoutes;

namespace OnibusExpress.Api.Controllers;

[ApiController]
[Route("rotas")]
public class RoutesController : ControllerBase
{
    private readonly IListRoutesUseCase _listRoutesUseCase;

    public RoutesController(IListRoutesUseCase listRoutesUseCase)
    {
        _listRoutesUseCase = listRoutesUseCase;
    }

    [HttpGet]
    [ProducesResponseType<ListRoutesResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ResponseError>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ListRoutes(CancellationToken cancellationToken)
    {
        var response = await _listRoutesUseCase.ExecuteAsync(new ListRoutesRequest(), cancellationToken);
        return Ok(response);
    }
}
