using Api.Models.ValueObjects;
using Api.Models;

public class DependentOverAgeRule : IBenefitCostRule
{
    public Money ApplyRule(Employee employee, BenefitCostConfig config, Money currentCost)
    {
        foreach (var dependent in employee.Dependents)
        {
            if (dependent.IsOverAge(config.DependentOverAgeThreshold))
            {
                currentCost = currentCost.Add(new Money(config.DependentOverAgeCost));
            }
        }
        return currentCost;
    }
}
