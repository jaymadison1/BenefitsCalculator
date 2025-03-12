namespace Api.Models.ValueObjects;

public class BenefitCostConfig
{
    public Money EmployeeBaseCost { get; }
    public Money DependentBaseCost { get; }
    public Money Over50ExtraCost { get; }
    public Money HighEarnerThreshold { get; }
    public decimal HighEarnerPenaltyRate { get; } // 2% = 0.02

    public BenefitCostConfig(Money employeeBaseCost, Money dependentBaseCost, Money over50ExtraCost, Money highEarnerThreshold, decimal highEarnerPenaltyRate)
    {
        EmployeeBaseCost = employeeBaseCost;
        DependentBaseCost = dependentBaseCost;
        Over50ExtraCost = over50ExtraCost;
        HighEarnerThreshold = highEarnerThreshold;
        HighEarnerPenaltyRate = highEarnerPenaltyRate;
    }
}
