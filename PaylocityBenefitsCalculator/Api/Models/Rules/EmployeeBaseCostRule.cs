using Api.Models.ValueObjects;
using Api.Models;

public class EmployeeBaseCostRule : IBenefitCostRule
{
    public Money ApplyRule(Employee employee, BenefitCostConfig config, Money currentCost)
    {
        return currentCost.Add(new Money(config.EmployeeBaseCost));
    }
}
