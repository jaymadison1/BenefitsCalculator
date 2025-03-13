using Api.Models.ValueObjects;
using Api.Models;

public interface IBenefitCostRule
{
    Money ApplyRule(Employee employee, BenefitCostConfig config, Money currentCost);
}
