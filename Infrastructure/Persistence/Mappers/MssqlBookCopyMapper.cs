using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence.Entities;
using System;

namespace Infrastructure.Persistence.Mappers
{
  public class MssqlBookCopyMapper : IEntityMapper<BookCopy, MssqlBookCopy>
  {
    public BookCopy ToDomain(MssqlBookCopy persistence)
    {
      return new BookCopy
      {
        Id = persistence.Id,
        Book = new Book { Id = persistence.BookId },
        Library = new Library { Id = persistence.LibraryId },
        Borrower = persistence.BorrowerId != null ? new Customer { Id = persistence.BorrowerId.Value } : null,
      };
    }

    public MssqlBookCopy ToPersistence(BookCopy domain)
    {
      return new MssqlBookCopy
      {
        Id = domain.Id,
        BookId = domain.Book.Id,
        LibraryId = domain.Library.Id,
        BorrowerId = domain.Borrower?.Id
      };
    }
  }
}
