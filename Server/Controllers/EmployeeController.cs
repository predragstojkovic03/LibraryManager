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

    public List<Employee> GetEmployeeList()
    {
      return _employeeService.GetAll();
    }

    public Employee GetEmployeeById(Guid id)
    {
      return _employeeService.GetById(id);
    }

    public Employee CreateEmployee(EmployeeDto employeeDto)
    {
      var employee = new Employee
      {
        FirstName = employeeDto.FirstName,
        LastName = employeeDto.LastName,
        Username = employeeDto.Username,
        Password = employeeDto.Password,
        Library = new Library { Id = employeeDto.LibraryId }
      };
      return _employeeService.Create(employee);
    }

    public Employee UpdateEmployee(EmployeeDto employeeDto)
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
      return _employeeService.Update(employee);
    }
  }
}