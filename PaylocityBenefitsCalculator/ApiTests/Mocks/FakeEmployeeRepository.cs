using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTests.Mocks;
public class FakeEmployeeRepository : IEmployeeRepository
{
    private readonly List<Employee> _employees = new()
    {
        new Employee { Id = 1, Salary = 78000m, Dependents = new List<Dependent>
            {
                new Dependent { Id = 1, Relationship = Relationship.Spouse,  DateOfBirth = new DateTime(1998, 1, 1) },
                new Dependent { Id = 2, Relationship = Relationship.Child,  DateOfBirth = new DateTime(2005, 1, 1) }
            }
        },
        new Employee { Id = 2, Salary = 52000m, Dependents = new List<Dependent>() }
    };

    public Task<List<Employee>> GetAllAsync() => Task.FromResult(_employees);

    public Task<Employee?> GetByIdAsync(int id) =>
        Task.FromResult(_employees.FirstOrDefault(e => e.Id == id));
}
