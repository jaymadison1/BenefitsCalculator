using Api.Models;
using System.Text.Json;
using System.Text.Json.Serialization;


public class DependentRepository : IDependentRepository
{
    //[Jay]  I wanted to have a repository call to pull data even though for now it is json.  This can be modified to pull data from a data source (external api, entity framework, ado.net, etc)
    private readonly string _jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "employees.json");

    public async Task<List<Dependent>> GetAllAsync()
    {
        if (!File.Exists(_jsonFilePath))
            return new List<Dependent>();

        var jsonData = await File.ReadAllTextAsync(_jsonFilePath);
        var employees = JsonSerializer.Deserialize<List<Employee>>(jsonData, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        });

        return employees?
            .SelectMany(e => e.Dependents)
            .ToList() ?? new List<Dependent>();
    }

    public async Task<Dependent?> GetByIdAsync(int dependentId)
    {
        var dependents = await GetAllAsync();
        return dependents.FirstOrDefault(d => d.Id == dependentId);
    }
}
