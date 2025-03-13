namespace Api.Models.ValueObjects;


//[Jay] Having a salary value object helps align with the paycheck domain terminology, enforce business rules, etc
public class Salary
{
    public Money Yearly { get; }

    public Salary(Money yearly)
    {
        if (yearly.Amount < 0)
            throw new ArgumentException("Salary cannot be negative.");
        Yearly = yearly;
    }
}
