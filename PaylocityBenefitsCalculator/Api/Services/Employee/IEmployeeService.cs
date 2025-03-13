using Api.Dtos.Employee;

public interface IEmployeeService
{
    Task<List<GetEmployeeDto>> GetAllEmployeesAsync();
    Task<GetEmployeeDto?> GetEmployeeByIdAsync(int id);
}
