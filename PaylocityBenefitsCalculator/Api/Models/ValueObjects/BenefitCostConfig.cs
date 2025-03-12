namespace Api.Models.ValueObjects;

public class BenefitCostConfig
{
    public decimal EmployeeBaseCost { get; set; } = 0.0m;
    public decimal DependentBaseCost { get; set; } = 0.0m;
    public decimal Over50ExtraCost { get; set; } = 0.0m;
    public decimal HighEarnerThreshold { get; set; } = 0.0m;
    public decimal HighEarnerPenaltyRate { get; set; } = 0.0m;

    public BenefitCostConfig() { }

    public BenefitCostConfig(decimal employeeBaseCost, decimal dependentBaseCost, decimal over50ExtraCost, decimal highEarnerThreshold, decimal highEarnerPenaltyRate)
    {
        EmployeeBaseCost = employeeBaseCost;
        DependentBaseCost = dependentBaseCost;
        Over50ExtraCost = over50ExtraCost;
        HighEarnerThreshold = highEarnerThreshold;
        HighEarnerPenaltyRate = highEarnerPenaltyRate;
    }

    public Money GetEmployeeBaseCost() => new Money(EmployeeBaseCost);
    public Money GetDependentBaseCost() => new Money(DependentBaseCost);
    public Money GetOver50ExtraCost() => new Money(Over50ExtraCost);
    public Money GetHighEarnerThreshold() => new Money(HighEarnerThreshold);
}
