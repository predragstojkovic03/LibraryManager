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

    public async Task<List<BookCopy>> GetBookCopyListAsync()
    {
      return await Task.Run(() => _bookCopyService.GetAll());
    }

    public async Task<BookCopy> GetBookCopyByIdAsync(Guid id)
    {
      return await Task.Run(() => _bookCopyService.GetById(id));
    }

    public async Task<BookCopy> CreateBookCopyAsync(BookCopyDto bookCopyDto)
    {
      var bookCopy = new BookCopy
      {
        Library = new Library { Id = bookCopyDto.LibraryId },
        Book = new Book { Id = bookCopyDto.BookId },
        Borrower = new Customer { Id = bookCopyDto.BorrowerId },
        PrintDate = bookCopyDto.PrintDate
      };
      return await Task.Run(() => _bookCopyService.Create(bookCopy));
    }

    public async Task<BookCopy> UpdateBookCopyAsync(BookCopyDto bookCopyDto)
    {
      var bookCopy = new BookCopy
      {
        Id = bookCopyDto.Id,
        Library = new Library { Id = bookCopyDto.LibraryId },
        Book = new Book { Id = bookCopyDto.BookId },
        Borrower = new Customer { Id = bookCopyDto.BorrowerId },
        PrintDate = bookCopyDto.PrintDate
      };
      return await Task.Run(() => _bookCopyService.Update(bookCopy));
    }
  }
}
