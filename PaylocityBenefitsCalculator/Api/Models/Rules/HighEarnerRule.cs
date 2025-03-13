using Api.Models.ValueObjects;
using Api.Models;

public class HighEarnerRule : IBenefitCostRule
{
    public Money ApplyRule(Employee employee, BenefitCostConfig config, Money currentCost)
    {
        if (employee.Salary > config.HighEarnerThreshold)
        {
            Money extraYearlyCost = new Money(employee.Salary * config.HighEarnerCostRate);
            Money extraMonthlyCost = new Money(extraYearlyCost.Amount / 12);
            currentCost = currentCost.Add(extraMonthlyCost);
        }
        return currentCost;
    }
}
