using Api.Models;
using System.Text.Json;
using System.Text.Json.Serialization;


public class EmployeeRepository : IEmployeeRepository
{

    //[Jay]  I wanted to have a repository call to pull data even though for now it is json.  This can be modified to pull data from a data source (external api, entity framework, ado.net, etc)
    private readonly string _jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "employees.json");

    public async Task<List<Employee>> GetAllAsync()
    {
        if (!File.Exists(_jsonFilePath))
            return new List<Employee>(); // Return an empty list if file doesn't exist

        var jsonData = await File.ReadAllTextAsync(_jsonFilePath);
        var employees = JsonSerializer.Deserialize<List<Employee>>(jsonData, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() } // ✅ Converts enums from strings
        });

        return employees ?? new List<Employee>();
    }

    public async Task<Employee?> GetByIdAsync(int id)
    {
        var employees = await GetAllAsync();
        return employees.FirstOrDefault(e => e.Id == id);
    }
}
