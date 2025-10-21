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

    public async Task<List<Book>> GetBookListAsync()
    {
      return await Task.Run(() => _bookService.GetAll());
    }

    public async Task<Book> GetBookByIdAsync(Guid id)
    {
      return await Task.Run(() => _bookService.GetById(id));
    }

    public async Task<Book> CreateBookAsync(BookDto bookDto)
    {
      var book = new Book
      {
        Title = bookDto.Title,
        Author = bookDto.Author,
        Description = bookDto.Description
      };
      return await Task.Run(() => _bookService.Create(book));
    }

    public async Task<Book> UpdateBookAsync(BookDto bookDto)
    {
      var book = new Book
      {
        Id = bookDto.Id,
        Title = bookDto.Title,
        Author = bookDto.Author,
        Description = bookDto.Description
      };
      return await Task.Run(() => _bookService.Update(book));
    }
  }
}
