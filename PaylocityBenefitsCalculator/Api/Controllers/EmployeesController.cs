using Api.Dtos.Employee;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

[ApiController]
[Route("api/v1/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeesController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [SwaggerOperation(Summary = "Get employee by ID")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> Get(int id)
    {
        var employee = await _employeeService.GetEmployeeByIdAsync(id);

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

    [SwaggerOperation(Summary = "Get all employees")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetEmployeeDto>>>> GetAll()
    {
        var employees = await _employeeService.GetAllEmployeesAsync();

        if (employees == null || employees.Count == 0)
        {
            return NotFound(new ApiResponse<List<GetEmployeeDto>>
            {
                Success = false,
                Message = "No employees found."
            });
        }

        return new ApiResponse<List<GetEmployeeDto>>
        {
            Data = employees,
            Success = true
        };
    }
}
