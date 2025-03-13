using Api.Models.ValueObjects;
using Api.Models;

public class DependentCostRule : IBenefitCostRule
{
    public Money ApplyRule(Employee employee, BenefitCostConfig config, Money currentCost)
    {
        foreach (var dependent in employee.Dependents)
        {
            currentCost = currentCost.Add(new Money(config.DependentBaseCost));
        }
        return currentCost;
    }
}
