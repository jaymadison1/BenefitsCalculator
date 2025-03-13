using Api.Models;

public interface IDependentRepository
{
    Task<List<Dependent>> GetAllAsync();
    Task<Dependent?> GetByIdAsync(int dependentId);
}
