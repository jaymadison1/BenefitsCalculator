using Api.Models.ValueObjects;
using System.Threading.Tasks;

namespace ApiTests.Mocks;

public class FakeBenefitCostRepository : IBenefitCostRepository
{
    public Task<BenefitCostConfig> GetBenefitCostConfigAsync()
    {
        return Task.FromResult(new BenefitCostConfig
        {
            EmployeeBaseCost = 1000m,
            DependentBaseCost = 600m,
            DependentOverAgeThreshold = 50,
            DependentOverAgeCost = 200m,
            HighEarnerThreshold = 80000m,
            HighEarnerCostRate = 0.02m
        });
    }
}
