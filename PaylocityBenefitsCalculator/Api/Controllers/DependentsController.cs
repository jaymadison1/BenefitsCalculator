using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DependentsController : ControllerBase
{
    private readonly string _jsonEmployeesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "employees.json");

    [SwaggerOperation(Summary = "Get dependent by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetDependentDto>>> Get(int id)
    {
        if (!System.IO.File.Exists(_jsonEmployeesPath))
        {
            return NotFound(new ApiResponse<GetDependentDto>
            {
                Success = false,
                Message = "Employee data file not found."
            });
        }

        try
        {
            var jsonData = await System.IO.File.ReadAllTextAsync(_jsonEmployeesPath);
            var employees = JsonSerializer.Deserialize<List<GetEmployeeDto>>(jsonData, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            });

            // Extract all dependents
            var dependents = employees?
                .Where(e => e.Dependents != null) 
                .SelectMany(e => e.Dependents) 
                .ToList() ?? new List<GetDependentDto>();

            // Find the dependent by ID
            var dependent = dependents.FirstOrDefault(d => d.Id == id);

            if (dependent == null)
            {
                return NotFound(new ApiResponse<GetDependentDto>
                {
                    Success = false,
                    Message = $"Dependent with ID {id} not found."
                });
            }

            return new ApiResponse<GetDependentDto>
            {
                Data = dependent,
                Success = true
            };
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse<GetDependentDto>
            {
                Success = false,
                Message = $"Error reading dependent data: {ex.Message}"
            });
        }
    }

    [SwaggerOperation(Summary = "Get all dependents")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetDependentDto>>>> GetAll()
    {
        if (!System.IO.File.Exists(_jsonEmployeesPath))
        {
            return NotFound(new ApiResponse<List<GetDependentDto>>
            {
                Success = false,
                Message = "Employee data file not found."
            });
        }

        try
        {
            var jsonData = await System.IO.File.ReadAllTextAsync(_jsonEmployeesPath);
            var employees = JsonSerializer.Deserialize<List<GetEmployeeDto>>(jsonData, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            });

            // Extract all dependents finding all employees that have dependents and then flatten the list
            var dependents = employees?
                .Where(e => e.Dependents != null) 
                .SelectMany(e => e.Dependents) 
                .ToList() ?? new List<GetDependentDto>();

            return new ApiResponse<List<GetDependentDto>>
            {
                Data = dependents,
                Success = true
            };
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse<List<GetDependentDto>>
            {
                Success = false,
                Message = $"Error reading dependent data: {ex.Message}"
            });
        }
    }
}
