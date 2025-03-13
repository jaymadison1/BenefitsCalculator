using Api.Models.ValueObjects;

public interface IBenefitCostRepository
{
    Task<BenefitCostConfig> GetBenefitCostConfigAsync();
}