using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Services;
using Domain.Entities;
using Server.Dtos;

namespace Server.Controllers
{
  public class BookCopyController
  {
    private readonly CrudService<BookCopy> _bookCopyService;

    public BookCopyController(CrudService<BookCopy> bookCopyService)
    {
      _bookCopyService = bookCopyService;
    }

    public List<BookCopy> GetBookCopyList()
    {
      return _bookCopyService.GetAll();
    }

    public BookCopy GetBookCopyById(Guid id)
    {
      return _bookCopyService.GetById(id);
    }

    public BookCopy CreateBookCopy(BookCopyDto bookCopyDto)
    {
      var bookCopy = new BookCopy
      {
        Library = new Library { Id = bookCopyDto.LibraryId },
        Book = new Book { Id = bookCopyDto.BookId },
        Borrower = new Customer { Id = bookCopyDto.BorrowerId },
        PrintDate = bookCopyDto.PrintDate
      };
      return _bookCopyService.Create(bookCopy);
    }

    public BookCopy UpdateBookCopy(BookCopyDto bookCopyDto)
    {
      var bookCopy = new BookCopy
      {
        Id = bookCopyDto.Id,
        Library = new Library { Id = bookCopyDto.LibraryId },
        Book = new Book { Id = bookCopyDto.BookId },
        Borrower = new Customer { Id = bookCopyDto.BorrowerId },
        PrintDate = bookCopyDto.PrintDate
      };
      return _bookCopyService.Update(bookCopy);
    }
  }
}
