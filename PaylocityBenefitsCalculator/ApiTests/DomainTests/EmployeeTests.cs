using System;
using Api.Models;
using Xunit;
namespace ApiTests.DomainTests;

public class EmployeeUnitTests
{

    [Fact]
    public void AddDependent_ShouldThrowException_WhenAddingBothSpouseAndDomesticPartner()
    {
        var employee = new Employee { Id = 1, FirstName = "LeBron", LastName = "James", Salary = 75000m, DateOfBirth = new DateTime(1984, 12, 30) };
        var spouse = new Dependent { Id = 1, FirstName = "Savannah", LastName = "James", Relationship = Relationship.Spouse, DateOfBirth = new DateTime(1986, 8, 27), EmployeeId = employee.Id };
        var domesticPartner = new Dependent { Id = 2, FirstName = "DP", LastName = "James", Relationship = Relationship.DomesticPartner, DateOfBirth = new DateTime(1985, 1, 1), EmployeeId = employee.Id };

        employee.AddDependent(spouse);

        var exception = Assert.Throws<InvalidOperationException>(() => employee.AddDependent(domesticPartner));
        Assert.Equal("An employee may only have one spouse OR one domestic partner (not both).", exception.Message);
    }

    [Fact]
    public void AddDependent_ShouldAllowOnlyOneSpouse()
    {
        var employee = new Employee { Id = 3, FirstName = "Kobe", LastName = "Bryant", Salary = 120000m, DateOfBirth = new DateTime(1978, 8, 23) };
        var spouse1 = new Dependent { Id = 5, FirstName = "Vanessa", LastName = "Bryant", Relationship = Relationship.Spouse, DateOfBirth = new DateTime(1982, 5, 5), EmployeeId = employee.Id };
        var spouse2 = new Dependent { Id = 6, FirstName = "Another", LastName = "Person", Relationship = Relationship.Spouse, DateOfBirth = new DateTime(1980, 3, 10), EmployeeId = employee.Id };

        employee.AddDependent(spouse1);

        var exception = Assert.Throws<InvalidOperationException>(() => employee.AddDependent(spouse2));
        Assert.Equal("An employee may only have one spouse OR one domestic partner (not both).", exception.Message);
    }

    [Fact]
    public void AddDependent_ShouldAllowMultipleChildren()
    {
        var employee = new Employee { Id = 2, FirstName = "Michael", LastName = "Jordan", Salary = 100000m, DateOfBirth = new DateTime(1963, 2, 17) };
        var child1 = new Dependent { Id = 3, FirstName = "Child1", LastName = "Jordan", Relationship = Relationship.Child, DateOfBirth = new DateTime(2000, 6, 15), EmployeeId = employee.Id };
        var child2 = new Dependent { Id = 4, FirstName = "Child2", LastName = "Jordan", Relationship = Relationship.Child, DateOfBirth = new DateTime(2003, 4, 20), EmployeeId = employee.Id };

        employee.AddDependent(child1);
        employee.AddDependent(child2);

        Assert.Contains(child1, employee.Dependents);
        Assert.Contains(child2, employee.Dependents);
    }
}

