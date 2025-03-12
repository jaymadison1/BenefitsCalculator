namespace Api.Models.ValueObjects;

public class Money
{
    public decimal Amount { get; }
    public Money(decimal amount)
    {
        if (amount < 0)
            throw new ArgumentException("Money amount cannot be negative.");
     
        Amount = Math.Round(amount, 2, MidpointRounding.AwayFromZero);
    }

    public Money Add(Money other) => new Money(this.Amount + other.Amount);

    public Money Subtract(Money other)
    {
        if (this.Amount < other.Amount)
            throw new InvalidOperationException("Resulting amount would be negative.");
        return new Money(this.Amount - other.Amount);
    }

    public Money Multiply(decimal factor) => new Money(this.Amount * factor);


    //Override Equals() to compare values instead of references
    public override bool Equals(object? obj)
    {
        if (obj is Money other)
        {
            return Amount == other.Amount;
        }
        return false;
    }

    // Ensures two Money instances with the same Amount have the same hash code
    public override int GetHashCode()
    {
        return Amount.GetHashCode();
    }
}
