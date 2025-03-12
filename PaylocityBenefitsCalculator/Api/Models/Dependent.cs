namespace Api.Models;

public class Dependent
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Relationship Relationship { get; set; }
    public int EmployeeId { get; set; }
    public Employee? Employee { get; set; }

    public bool IsOverAge(int ageThreshold)
    {
        int age = DateTime.Today.Year - DateOfBirth.Year;

        if (DateTime.Today < DateOfBirth.AddYears(age))
        {
            age--;
        }

        return age >= ageThreshold;
    }
}
