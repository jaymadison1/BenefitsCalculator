using Api.Dtos.Dependent;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

[ApiController]
[Route("api/v1/[controller]")]
public class DependentsController : ControllerBase
{
    private readonly IDependentService _dependentService;

    public DependentsController(IDependentService dependentService)
    {
        _dependentService = dependentService;
    }

    [SwaggerOperation(Summary = "Get all dependents")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetDependentDto>>>> GetAll()
    {
        var dependents = await _dependentService.GetAllDependentsAsync();

        if (dependents == null || dependents.Count == 0)
        {
            return NotFound(new ApiResponse<List<GetDependentDto>>
            {
                Success = false,
                Message = "No dependents found."
            });
        }

        return new ApiResponse<List<GetDependentDto>>
        {
            Data = dependents,
            Success = true
        };
    }

    [SwaggerOperation(Summary = "Get dependent by ID")]
    [HttpGet("{dependentId}")]
    public async Task<ActionResult<ApiResponse<GetDependentDto>>> Get(int dependentId)
    {
        var dependent = await _dependentService.GetDependentByIdAsync(dependentId);

        if (dependent == null)
        {
            return NotFound(new ApiResponse<GetDependentDto>
            {
                Success = false,
                Message = $"Dependent with ID {dependentId} not found."
            });
        }

        return new ApiResponse<GetDependentDto>
        {
            Data = dependent,
            Success = true
        };
    }
}
