using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<List<GetEmployeeDto>> GetAllEmployeesAsync()
    {
        var employees = await _employeeRepository.GetAllAsync();
        return employees.Select(e => MapToDto(e)).ToList();
    }

    public async Task<GetEmployeeDto?> GetEmployeeByIdAsync(int id)
    {
        var employee = await _employeeRepository.GetByIdAsync(id);
        return employee != null ? MapToDto(employee) : null;
    }

    private static GetEmployeeDto MapToDto(Employee employee)
    {
        return new GetEmployeeDto
        {
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Salary = employee.Salary,
            DateOfBirth = employee.DateOfBirth,
            Dependents = employee.Dependents?.Select(d => new GetDependentDto
            {
                Id = d.Id,
                FirstName = d.FirstName,
                LastName = d.LastName,
                Relationship = d.Relationship,
                DateOfBirth = d.DateOfBirth
            }).ToList() ?? new List<GetDependentDto>()
        };
    }
}
