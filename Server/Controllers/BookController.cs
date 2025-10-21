using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Services;
using Domain.Entities;
using Server.Dtos;

namespace Server.Controllers
{
  public class BookController
  {
    private readonly CrudService<Book> _bookService;

    public BookController(CrudService<Book> bookService)
    {
      _bookService = bookService;
    }

    public List<Book> GetBookList()
    {
      return _bookService.GetAll();
    }

    public Book GetBookById(Guid id)
    {
      return _bookService.GetById(id);
    }

    public Book CreateBook(BookDto bookDto)
    {
      var book = new Book
      {
        Title = bookDto.Title,
        Author = bookDto.Author,
        Description = bookDto.Description
      };
      return _bookService.Create(book);
    }

    public Book UpdateBook(BookDto bookDto)
    {
      var book = new Book
      {
        Id = bookDto.Id,
        Title = bookDto.Title,
        Author = bookDto.Author,
        Description = bookDto.Description
      };
      return _bookService.Update(book);
    }
  }
}
