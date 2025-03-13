namespace Api.Models.ValueObjects;

public class BenefitCost
{
    public Money MonthlyCost { get; }
    public Money AnnualCost { get; }
    public Money PerPaycheckCost { get; }

    //[Jay] Encapsulate the benefit cost logic to keep all calculations in one place.

    public BenefitCost(Employee employee, BenefitCostConfig config, List<IBenefitCostRule> rules)
    {
        Money monthlyCost = new Money(0);

        foreach (var rule in rules)
        {
            monthlyCost = rule.ApplyRule(employee, config, monthlyCost);
        }

        MonthlyCost = monthlyCost;
        AnnualCost = new Money(MonthlyCost.Amount * 12);

        //[Jay]  Calculate per paycheck cost by dividing the annual cost over 26 paychecks.  
        //[Jay]  TODO: Review options to handle potential rounding issues (ie round up in favor of employee, amend the last paycheck, etc)

        decimal rawPerPaycheck = AnnualCost.Amount / 26;

        PerPaycheckCost = new Money(rawPerPaycheck);
    }

}
