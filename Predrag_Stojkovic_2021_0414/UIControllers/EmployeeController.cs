using System;
using System.Collections.Generic;
using Client.Server;
using Domain.Entities;
using Infrastructure.Communication;

namespace Predrag_Stojkovic_2021_0414.UIControllers
{
  public class EmployeeController
  {
    private readonly ServerAdapter _serverAdapter;

    public EmployeeController(ServerAdapter serverAdapter)
    {
      _serverAdapter = serverAdapter;
    }

    public List<Employee> GetEmployees(Guid libraryId)
    {
      var response = _serverAdapter.MakeRequest(Operation.EmployeesFindAll, null);
      var employees = new List<Employee>();
      if (response?.Data != null && response.Data.ToString() != null)
      {
        try
        {
          var allEmployees = System.Text.Json.JsonSerializer.Deserialize<List<Employee>>(response.Data.ToString());
          if (allEmployees != null)
          {
            foreach (var emp in allEmployees)
            {
              if (emp.Library != null && emp.Library.Id == libraryId)
                employees.Add(emp);
            }
          }
        }
        catch { }
      }
      return employees;
    }
  }
}
