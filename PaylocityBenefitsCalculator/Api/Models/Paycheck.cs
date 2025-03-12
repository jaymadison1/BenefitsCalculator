using Api.Models.ValueObjects;

namespace Api.Models;

public class Paycheck
{
    public Guid Id { get; }
    public int EmployeeId { get; private set; }
    public Money GrossPay { get; }
    public Money Deductions { get; }
    public Money NetPay => GrossPay.Subtract(Deductions);

    public Paycheck(int employeeId, Money grossPay, Money deductions)
    {
        if (grossPay.Amount < 0)
            throw new ArgumentException("Gross pay cannot be negative.");
        if (deductions.Amount < 0)
            throw new ArgumentException("Deductions cannot be negative.");

        Id = Guid.NewGuid();
        EmployeeId = employeeId;
        GrossPay = grossPay;
        Deductions = deductions;
    }
}
