using Api.Dtos.Dependent;
using Api.Models;

public class DependentService : IDependentService
{
    private readonly IDependentRepository _dependentRepository;

    public DependentService(IDependentRepository dependentRepository)
    {
        _dependentRepository = dependentRepository;
    }

    public async Task<List<GetDependentDto>> GetAllDependentsAsync()
    {
        var dependents = await _dependentRepository.GetAllAsync();
        return dependents.Select(d => MapToDto(d)).ToList();
    }

    public async Task<GetDependentDto?> GetDependentByIdAsync(int dependentId)
    {
        var dependent = await _dependentRepository.GetByIdAsync(dependentId);
        return dependent != null ? MapToDto(dependent) : null;
    }

    //[Jay] Convert dependent domain model to the dependent dto 
    private static GetDependentDto MapToDto(Dependent dependent)
    {
        return new GetDependentDto
        {
            Id = dependent.Id,
            FirstName = dependent.FirstName,
            LastName = dependent.LastName,
            Relationship = dependent.Relationship,
            DateOfBirth = dependent.DateOfBirth
        };
    }
}
