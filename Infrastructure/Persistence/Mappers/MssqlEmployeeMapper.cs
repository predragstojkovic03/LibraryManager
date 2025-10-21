using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence.Entities;
using System;

namespace Infrastructure.Persistence.Mappers
{
  public class MssqlEmployeeMapper : IEntityMapper<Employee, MssqlEmployee>
  {
    public Employee ToDomain(MssqlEmployee persistence)
    {
      return new Employee
      {
        Id = persistence.Id,
        FirstName = persistence.FirstName,
        LastName = persistence.LastName,
        Password = persistence.Password,
        Username = persistence.Username,
        Library = new Library { Id = persistence.LibraryId } // Should be loaded separately
      };
    }

    public MssqlEmployee ToPersistence(Employee domain)
    {
      return new MssqlEmployee
      {
        Id = domain.Id,
        FirstName = domain.FirstName,
        LastName = domain.LastName,
        Password = domain.Password,
        Username = domain.Username,
        // LibraryId should be set from domain.Library.Id if available
      };
    }
  }
}
