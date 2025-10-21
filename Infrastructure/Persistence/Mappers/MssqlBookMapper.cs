using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence.Entities;
using System;

namespace Infrastructure.Persistence.Mappers
{
  public class MssqlBookMapper : IEntityMapper<Book, MssqlBook>
  {
    public Book ToDomain(MssqlBook persistence)
    {
      return new Book
      {
        Id = persistence.Id,
        Title = persistence.Title ?? string.Empty,
        Author = persistence.Author ?? string.Empty,
        Description = persistence.Description ?? string.Empty,
        Copies = new List<BookCopy>() // Copies should be loaded separately
      };
    }

    public MssqlBook ToPersistence(Book domain)
    {
      return new MssqlBook
      {
        Id = domain.Id,
        Title = domain.Title,
        Author = domain.Author,
        Description = domain.Description
      };
    }
  }
}
