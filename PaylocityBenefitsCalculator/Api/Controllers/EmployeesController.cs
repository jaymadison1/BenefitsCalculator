using Api.Dtos.Employee;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly string _jsonEmployeesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "employees.json");

    [SwaggerOperation(Summary = "Get employee by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> Get(int id)
    {
        if (!System.IO.File.Exists(_jsonEmployeesPath))
        {
            return NotFound(new ApiResponse<GetEmployeeDto>
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

            var employee = employees?.FirstOrDefault(e => e.Id == id);

            if (employee == null)
            {
                return NotFound(new ApiResponse<GetEmployeeDto>
                {
                    Success = false,
                    Message = $"Employee with ID {id} not found."
                });
            }

            return new ApiResponse<GetEmployeeDto>
            {
                Data = employee,
                Success = true
            };
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse<GetEmployeeDto>
            {
                Success = false,
                Message = $"Error reading employee data: {ex.Message}"
            });
        }
    }

    [SwaggerOperation(Summary = "Get all employees")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetEmployeeDto>>>> GetAll()
    {
        //task: use a more realistic production approach
        // [Jay] Moved hardcoded data to JSON file to better mimic an external data store.

        if (!System.IO.File.Exists(_jsonEmployeesPath))
        {
            return NotFound(new ApiResponse<List<GetEmployeeDto>>
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

            return new ApiResponse<List<GetEmployeeDto>>
            {
                Data = employees,
                Success = true
            };
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse<List<GetEmployeeDto>>
            {
                Success = false,
                Message = $"Error reading employee data: {ex.Message}"
            });
        }
    }
}
