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
    private readonly string _jsonEmployeesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "employees.json");
    private readonly BenefitCostConfig _benefitCostConfig;

    //[Jay] TODO:  Load dynamic/client based configuration values from a repository/database
    //private readonly IBenefitCostRepository _benefitCostRepository;

    public PaycheckController(BenefitCostConfig benefitCostConfig)
    {
        _benefitCostConfig = benefitCostConfig;
    }

    [HttpGet("{employeeId}")]
    public ActionResult<GetPaycheckDto> GetPaycheck(int employeeId)
    {
        var employees = LoadEmployeesFromJson();
        var employee = employees.FirstOrDefault(e => e.Id == employeeId);

        if (employee == null)
            return NotFound($"Employee with ID {employeeId} not found.");

        //[Jay] TODO:  Load dynamic/client based configuration values from a repository/database
        //var benefitConfig = await _benefitCostRepository.GetBenefitCostConfigsAsync(payDate);

        var paycheck = employee.CalculatePaycheck(_benefitCostConfig);

        var paycheckDto = new GetPaycheckDto
        {
            EmployeeId = paycheck.EmployeeId,
            GrossPay = paycheck.GrossPay.Amount,
            Deductions = paycheck.Deductions.Amount,
            NetPay = paycheck.NetPay.Amount
        };

        return Ok(paycheckDto);
    }

    private List<Employee> LoadEmployeesFromJson()
    {
        if (!System.IO.File.Exists(_jsonEmployeesPath))
            return new List<Employee>(); // Return an empty list if file not found

        var jsonData = System.IO.File.ReadAllText(_jsonEmployeesPath);
        var employees = JsonSerializer.Deserialize<List<Employee>>(jsonData, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        });
        return employees ?? new List<Employee>();
    }
}
