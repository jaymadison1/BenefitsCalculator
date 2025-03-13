using Api.Dtos.Paycheck;
using Api.Models;

public interface IPaycheckService
{
    Task<GetPaycheckDto?> CalculatePaycheckAsync(int employeeId);
}