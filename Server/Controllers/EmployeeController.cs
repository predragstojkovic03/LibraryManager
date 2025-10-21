using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Services;
using Domain.Entities;
using Server.Dtos;

namespace Server.Controllers
{
  public class EmployeeController
  {
    private readonly CrudService<Employee> _employeeService;

    public EmployeeController(CrudService<Employee> employeeService)
    {
      _employeeService = employeeService;
    }

    public async Task<List<Employee>> GetEmployeeListAsync()
    {
      return await Task.Run(() => _employeeService.GetAll());
    }

    public async Task<Employee> GetEmployeeByIdAsync(Guid id)
    {
      return await Task.Run(() => _employeeService.GetById(id));
    }

    public async Task<Employee> CreateEmployeeAsync(EmployeeDto employeeDto)
    {
      var employee = new Employee
      {
        FirstName = employeeDto.FirstName,
        LastName = employeeDto.LastName,
        Username = employeeDto.Username,
        Password = employeeDto.Password,
        Library = new Library { Id = employeeDto.LibraryId }
      };
      return await Task.Run(() => _employeeService.Create(employee));
    }

    public async Task<Employee> UpdateEmployeeAsync(EmployeeDto employeeDto)
    {
      var employee = new Employee
      {
        Id = employeeDto.Id,
        FirstName = employeeDto.FirstName,
        LastName = employeeDto.LastName,
        Username = employeeDto.Username,
        Password = employeeDto.Password,
        Library = new Library { Id = employeeDto.LibraryId }
      };
      return await Task.Run(() => _employeeService.Update(employee));
    }
  }
}