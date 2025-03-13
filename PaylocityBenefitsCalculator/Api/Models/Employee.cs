using Api.Models.ValueObjects;

namespace Api.Models;

public class Employee
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public decimal Salary { get; set; }
    public DateTime DateOfBirth { get; set; }
    public ICollection<Dependent> Dependents { get; set; } = new List<Dependent>();

   
    public void AddDependent(Dependent dependent)
    {
        if (dependent == null) throw new ArgumentNullException(nameof(dependent));

        // Enforce Business Rule: Only 1 Spouse OR Domestic Partner
        if (dependent.Relationship == Relationship.Spouse || dependent.Relationship == Relationship.DomesticPartner)
        {
            bool hasSpouseOrPartner = Dependents.Any(d => d.Relationship == Relationship.Spouse || d.Relationship == Relationship.DomesticPartner);
            if (hasSpouseOrPartner)
                throw new InvalidOperationException("An employee may only have one spouse OR one domestic partner (not both).");
        }

        Dependents.Add(dependent);
    }

    public void RemoveDependent(int dependentId)
    {
        var dependent = Dependents.FirstOrDefault(d => d.Id == dependentId);
        if (dependent != null)
        {
            Dependents.Remove(dependent);
        }
    }
}
