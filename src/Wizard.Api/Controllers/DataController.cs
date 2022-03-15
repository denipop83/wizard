using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Wizard.App.Features;
using Wizard.App.Models;

namespace Wizard.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}")]
public class DataController : Controller
{
    private readonly IMediator _mediator;

    public DataController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Retrieves all countries
    /// </summary>
    /// <response code="200">Countries successfully retrieved</response>
    /// <response code="400">Somehow there are not countries :-(</response>
    [Route("countries")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<CountriesResponse>> GetCountriesAsync() =>
        Ok(await _mediator.Send(new GetCountries.Query()));

    /// <summary>
    /// Retrieves country provinces by country id
    /// </summary>
    /// <response code="200">Provinces successfully retrieved</response>
    /// <response code="400">Can't find provinces with specified id</response>
    [Route("provinces")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<ProvincesResponse>> GetProvincesAsync(int countryId) =>
        Ok(await _mediator.Send(new GetProvinces.Query(countryId)));

    
    /// <summary>
    /// Adds new registration
    /// </summary>
    /// <response code="200">New registration successfully added</response>
    /// <response code="404">Something wen wrong</response>
    [Route("add-registration")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<AddRegistrationResponse>> AddRegistrationAsync(AddRegistrationRequest addRegistration)
    {
        var command = addRegistration.Adapt<AddRegistration.Command>();
        var result = await _mediator.Send(command);
        return result switch
        {
            { Email: { } } => Ok(result),
            _ => BadRequest(result)
        };
    }
}