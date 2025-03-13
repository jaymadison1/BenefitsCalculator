using Api.Dtos.Dependent;

public interface IDependentService
{
    Task<List<GetDependentDto>> GetAllDependentsAsync();
    Task<GetDependentDto?> GetDependentByIdAsync(int dependentId);
}
