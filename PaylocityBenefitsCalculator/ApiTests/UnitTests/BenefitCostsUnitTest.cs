using System;
using System.Collections.Generic;
using Api.Models;
using Api.Models.ValueObjects;
using Xunit;
namespace ApiTests.UnitTests;

public class BenefitCostsUnitTest
{
    [Fact]
    public void BenefitCost_ShouldApplyBaseEmployeeCost()
    {
        var config = new BenefitCostConfig
        {
            EmployeeBaseCost = 1000m,
            DependentBaseCost = 600m,
            DependentOverAgeThreshold = 50,
            DependentOverAgeCost = 200m,
            HighEarnerThreshold = 80000m,
            HighEarnerCostRate = 0.02m
        };

        var salary = new Salary(new Money(70000m)); // Below high earner threshold
        var dependents = new List<Dependent>();     // No dependents

        var benefitCost = new BenefitCost(salary, dependents, config);

        Assert.Equal(new Money(1000m), benefitCost.MonthlyCost);
        Assert.Equal(new Money(12000m), benefitCost.AnnualCost);
        Assert.Equal(new Money(12000m / 26), benefitCost.PerPaycheckCost);
    }

    [Fact]
    public void BenefitCost_ShouldChargeForEachDependent()
    {
        var config = new BenefitCostConfig
        {
            EmployeeBaseCost = 1000m,
            DependentBaseCost = 600m,
            DependentOverAgeThreshold = 50,
            DependentOverAgeCost = 200m,
            HighEarnerThreshold = 80000m,
            HighEarnerCostRate = 0.02m
        };

        var salary = new Salary(new Money(70000m));
        var dependents = new List<Dependent>
        {
            new Dependent { DateOfBirth = new DateTime(2005, 1, 1) }, // 19 years old (no extra charge)
            new Dependent { DateOfBirth = new DateTime(1960, 1, 1) }  // 65 years old (extra charge applies)
        };

        var benefitCost = new BenefitCost(salary, dependents, config);

        Assert.Equal(new Money(1000m + 600m + 600m + 200m), benefitCost.MonthlyCost); // 2 dependents, 1 incurs extra cost
        Assert.Equal(new Money((1000m + 600m + 600m + 200m) * 12), benefitCost.AnnualCost);
        Assert.Equal(new Money(benefitCost.AnnualCost.Amount / 26), benefitCost.PerPaycheckCost);
    }

    [Fact]
    public void BenefitCost_ShouldApplyHighEarnerPenalty()
    {
        var config = new BenefitCostConfig
        {
            EmployeeBaseCost = 1000m,
            DependentBaseCost = 600m,
            DependentOverAgeThreshold = 50,
            DependentOverAgeCost = 200m,
            HighEarnerThreshold = 80000m,
            HighEarnerCostRate = 0.02m
        };

        var salary = new Salary(new Money(90000m)); // Above high earner threshold
        var dependents = new List<Dependent>();

        var benefitCost = new BenefitCost(salary, dependents, config);

        var extraYearlyCost = 90000m * config.HighEarnerCostRate;
        var extraMonthlyCost = new Money(extraYearlyCost / 12);

        Assert.Equal(new Money(1000m).Add(extraMonthlyCost), benefitCost.MonthlyCost);
        Assert.Equal(new Money(benefitCost.MonthlyCost.Amount * 12), benefitCost.AnnualCost);
    }

    [Fact]
    public void BenefitCost_ShouldHandleZeroDependents()
    {
        var config = new BenefitCostConfig
        {
            EmployeeBaseCost = 1000m,
            DependentBaseCost = 600m,
            DependentOverAgeThreshold = 50,
            DependentOverAgeCost = 200m,
            HighEarnerThreshold = 80000m,
            HighEarnerCostRate = 0.02m
        };

        var salary = new Salary(new Money(50000m)); // Below high earner threshold
        var dependents = new List<Dependent>(); // No dependents

        var benefitCost = new BenefitCost(salary, dependents, config);

        Assert.Equal(new Money(1000m), benefitCost.MonthlyCost);
        Assert.Equal(new Money(12000m), benefitCost.AnnualCost);
        Assert.Equal(new Money(12000m / 26), benefitCost.PerPaycheckCost);
    }

    [Fact]
    public void BenefitCost_ShouldApplyOverAgeChargeCorrectly()
    {
        var config = new BenefitCostConfig
        {
            EmployeeBaseCost = 1000m,
            DependentBaseCost = 600m,
            DependentOverAgeThreshold = 50, 
            DependentOverAgeCost = 200m, 
            HighEarnerThreshold = 80000m,
            HighEarnerCostRate = 0.02m
        };

        var salary = new Salary(new Money(70000m));
        var dependents = new List<Dependent>
        {
            new Dependent { DateOfBirth = DateTime.Today.AddYears(-55) } // 55 years old - over age threshold
        };

        var benefitCost = new BenefitCost(salary, dependents, config);

        decimal expectedMonthlyCost = 1000m + 600m + 200m; // Base + dependent cost + over age extra charge
        Assert.Equal(new Money(expectedMonthlyCost), benefitCost.MonthlyCost);
    }

    [Fact]
    public void BenefitCost_ShouldNotApplyOverAgeChargeForYoungerDependents()
    {
        var config = new BenefitCostConfig
        {
            EmployeeBaseCost = 1000m,
            DependentBaseCost = 600m,
            DependentOverAgeThreshold = 50,
            DependentOverAgeCost = 200m,
            HighEarnerThreshold = 80000m,
            HighEarnerCostRate = 0.02m
        };

        var salary = new Salary(new Money(70000m));
        var dependents = new List<Dependent>
        {
            new Dependent { DateOfBirth = DateTime.Today.AddYears(-45) } // test 45 years old (below threshold)
        };

        var benefitCost = new BenefitCost(salary, dependents, config);

        decimal expectedMonthlyCost = 1000m + 600m; // Base + 1 dependent cost (no over age dependent)
        Assert.Equal(new Money(expectedMonthlyCost), benefitCost.MonthlyCost);
    }
}

