﻿namespace Api.Models.ValueObjects;

public class BenefitCost
{
    public Money MonthlyCost { get; }
    public Money AnnualCost { get; }
    public Money PerPaycheckCost { get; }

    public BenefitCost(Salary salary, List<Dependent> dependents, BenefitCostConfig config)
    {
        // Start with the employee's base cost (set dynamically)
        Money cost = new Money(config.EmployeeBaseCost.Amount);

        //// Add cost for each dependent.
        //foreach (var dep in dependents)
        //{
        //    cost = cost.Add(config.DependentBaseCost);
        //    if (dep.IsOver50)
        //    {
        //        cost = cost.Add(config.Over50ExtraCost);
        //    }
        //}

        //// Add high earner penalty if applicable.
        //if (salary.Yearly.Amount > config.HighEarnerThreshold.Amount)
        //{
        //    // 2% of yearly salary added as benefits cost (spread monthly).
        //    Money extraYearlyCost = salary.Yearly.Multiply(config.HighEarnerPenaltyRate);
        //    Money extraMonthlyCost = new Money(extraYearlyCost.Amount / 12, config.EmployeeBaseCost.Currency);
        //    cost = cost.Add(extraMonthlyCost);
        //}

        MonthlyCost = cost;
        AnnualCost = new Money(MonthlyCost.Amount * 12);

        // Calculate per paycheck cost by dividing the annual cost over 26 paychecks.
        decimal rawPerPaycheck = AnnualCost.Amount / 26;
        PerPaycheckCost = new Money(rawPerPaycheck);
    }
}
