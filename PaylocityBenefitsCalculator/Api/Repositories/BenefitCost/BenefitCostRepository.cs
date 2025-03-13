using Api.Models.ValueObjects;
using System.Text.Json;



public class BenefitCostRepository : IBenefitCostRepository
{
    //[Jay]  Eventually we can pull benefit costs from a data source rather than json.  This will give flexibility to load config values dynamically

    private readonly string _jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "benefit-costs.json");
      

    public async Task<BenefitCostConfig> GetBenefitCostConfigAsync()
    {
        if (!File.Exists(_jsonFilePath))
            throw new FileNotFoundException($"Benefit cost configuration file not found: {_jsonFilePath}");

        var jsonData = await File.ReadAllTextAsync(_jsonFilePath);
        var benefitCostConfig = JsonSerializer.Deserialize<BenefitCostConfig>(jsonData, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return benefitCostConfig ?? throw new Exception("Invalid or missing benefit cost configuration.");
    }
}