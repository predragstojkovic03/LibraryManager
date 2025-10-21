using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence.Entities;
using System;

namespace Infrastructure.Persistence.Mappers
{
  public class MssqlCustomerMapper : IEntityMapper<Customer, MssqlCustomer>
  {
    public Customer ToDomain(MssqlCustomer persistence)
    {
      return new Customer
      {
        Id = persistence.Id,
        FirstName = persistence.FirstName ?? string.Empty,
        LastName = persistence.LastName ?? string.Empty,
        Email = persistence.Email,
        Phone = persistence.Phone ?? string.Empty,
        BorrowedCopies = new List<BookCopy>(), // Should be loaded separately
        Library = new Library { Id = persistence.LibraryId } // Should be loaded separately
      };
    }

    public MssqlCustomer ToPersistence(Customer domain)
    {
      return new MssqlCustomer
      {
        Id = domain.Id,
        FirstName = domain.FirstName,
        LastName = domain.LastName,
        Email = domain.Email,
        Phone = domain.Phone,
        LibraryId = domain.Library != null ? domain.Library.Id : Guid.Empty
      };
    }
  }
}
