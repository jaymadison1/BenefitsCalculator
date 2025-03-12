namespace Api.Models.ValueObjects;

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
