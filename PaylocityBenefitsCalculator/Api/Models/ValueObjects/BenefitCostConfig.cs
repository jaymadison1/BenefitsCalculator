namespace Api.Models.ValueObjects;

//[Jay] Decided to build out the rule config settings for a quick calculation test using a json data source.  

//[Jay] TODO: Instead of a config object, we can tie the config values in the BenefitCostRules

public class BenefitCostConfig
{
    public decimal EmployeeBaseCost { get; set; } = 0.0m;
    public decimal DependentBaseCost { get; set; } = 0.0m;
    public int DependentOverAgeThreshold { get; set; } = 0;
    public decimal DependentOverAgeCost { get; set; } = 0.0m;
    public decimal HighEarnerThreshold { get; set; } = 0.0m;
    public decimal HighEarnerCostRate { get; set; } = 0.0m;

    public BenefitCostConfig() { }

    public BenefitCostConfig(decimal employeeBaseCost, decimal dependentBaseCost, int dependentOverAgeThreshold, decimal dependentOverAgeCost, decimal highEarnerThreshold, decimal highEarnerCostRate)
    {
        EmployeeBaseCost = employeeBaseCost;
        DependentBaseCost = dependentBaseCost;
        DependentOverAgeThreshold = dependentOverAgeThreshold;
        DependentOverAgeCost = dependentOverAgeCost;
        HighEarnerThreshold = highEarnerThreshold;
        HighEarnerCostRate = highEarnerCostRate;
    }

    public Money GetEmployeeBaseCost() => new Money(EmployeeBaseCost);
    public Money GetDependentBaseCost() => new Money(DependentBaseCost);
    public Money GetDependentOverAgeCost() => new Money(DependentOverAgeCost);
    public Money GetHighEarnerThreshold() => new Money(HighEarnerThreshold);
}
