using Api.Dtos.Paycheck;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using Api.Models.ValueObjects;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/paychecks")]
public class PaycheckController : ControllerBase
{
    private readonly IPaycheckService _paycheckService;

    public PaycheckController(IPaycheckService paycheckService)
    {
        _paycheckService = paycheckService;
    }

    [HttpGet("{employeeId}")]
    public async Task<ActionResult<GetPaycheckDto>> GetPaycheck(int employeeId)
    {
        var paycheck = await _paycheckService.CalculatePaycheckAsync(employeeId);
        if (paycheck == null)
            return NotFound($"Employee with ID {employeeId} not found.");

        return Ok(paycheck);
    }
}
