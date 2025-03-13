using Xunit;
using ApiTests.Mocks;
using System.Threading.Tasks;
using Api.Models.ValueObjects;

namespace ApiTests.UnitTests;

public class PaycheckServiceTests
{
    private readonly PaycheckService _paycheckService;

    public PaycheckServiceTests()
    {
        var employeeRepository = new FakeEmployeeRepository(); 
        var benefitCostRepository = new FakeBenefitCostRepository();

        _paycheckService = new PaycheckService(employeeRepository, benefitCostRepository);
    }

    [Fact]
    public async Task CalculatePaycheckAsync_EmployeeNotFound_ReturnsNull()
    {
        // Act
        var result = await _paycheckService.CalculatePaycheckAsync(99); // Non-existing ID

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CalculatePaycheckAsync_CorrectGrossPayCalculation()
    {
        // Act
        var result = await _paycheckService.CalculatePaycheckAsync(2); // Employee with $52,000 salary

        // Assert
        Assert.NotNull(result);
        Assert.Equal(new Money(52000m / 26), new Money(result!.GrossPay)); // Should be $2000
    }

    [Fact]
    public async Task CalculatePaycheckAsync_CorrectDeductionsCalculation()
    {
        var result = await _paycheckService.CalculatePaycheckAsync(1);

        Assert.NotNull(result);

        var expectedDeductions = new Money(1015.38m);
        Assert.Equal(expectedDeductions, new Money(result!.Deductions));
    }

    [Fact]
    public async Task CalculatePaycheckAsync_CorrectNetPayCalculation()
    {
        var result = await _paycheckService.CalculatePaycheckAsync(1);

        Assert.NotNull(result);

        var expectedNetPay = new Money(1984.62m);
        Assert.Equal(expectedNetPay, new Money(result!.NetPay));
    }


}
