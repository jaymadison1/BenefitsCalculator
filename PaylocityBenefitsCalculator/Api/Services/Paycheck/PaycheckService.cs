using Api.Models.ValueObjects;
using Api.Models;
using Api.Dtos.Paycheck;

public class PaycheckService : IPaycheckService
{

    private readonly IBenefitCostRepository _benefitCostRepository;
    private readonly IEmployeeRepository _employeeRepository;


    //[Jay] Injecting the repositories on the constructor allows us to test calculating paychecks with mock repositories  
    public PaycheckService(IEmployeeRepository employeeRepository, IBenefitCostRepository benefitCostRepository)
    {
        _benefitCostRepository = benefitCostRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task<GetPaycheckDto?> CalculatePaycheckAsync(int employeeId)
    {
        var employee = await _employeeRepository.GetByIdAsync(employeeId);
        if (employee == null) return null;

        var benefitCostConfig = await _benefitCostRepository.GetBenefitCostConfigAsync();

        //[Jay]  TODO:  Generate the rules list dynamically by customer, year, testing flags
        var benefitCostRules = new List<IBenefitCostRule> {
            new EmployeeBaseCostRule(),
            new DependentCostRule(),
            new HighEarnerRule(),
            new DependentOverAgeRule()
        };

        var salary = new Salary(new Money(employee.Salary));
        var benefitCost = new BenefitCost(employee, benefitCostConfig, benefitCostRules);

        var grossPay = new Money(salary.Yearly.Amount / 26);
        var deductions = benefitCost.PerPaycheckCost;
        var netPay = grossPay.Subtract(deductions);

        return new GetPaycheckDto
        {
            EmployeeId = employee.Id,
            GrossPay = grossPay.Amount,
            Deductions = deductions.Amount,
            NetPay = netPay.Amount
        };
    }
}