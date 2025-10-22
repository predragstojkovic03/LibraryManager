using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Client.Server;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace Predrag_Stojkovic_2021_0414.UIControllers
{
  public class GuestController
  {
    private readonly ServerAdapter _serverAdapter;

    public GuestController(ServerAdapter serverAdapter)
    {
      _serverAdapter = serverAdapter;
    }

    public List<(BookCopy copy, Book book, Library library)> SearchBooksAndLibraries(string query)
    {
      var copiesResponse = _serverAdapter.MakeRequest(Infrastructure.Communication.Operation.BookCopiesFindAll, null);
      var booksResponse = _serverAdapter.MakeRequest(Infrastructure.Communication.Operation.BooksFindAll, null);
      var results = new List<(BookCopy, Book, Library)>();
      List<BookCopy>? copies = null;
      List<Book>? books = null;
      List<Library> libraries = JsonSerializer.Deserialize<List<Library>>(_serverAdapter.MakeRequest(Infrastructure.Communication.Operation.LibrariesFindAll, null).Data.ToString());
      if (copiesResponse?.Data != null && copiesResponse.Data.ToString() != null)
      {
        try
        {
          copies = System.Text.Json.JsonSerializer.Deserialize<List<BookCopy>>(copiesResponse.Data.ToString());
        }
        catch { }
      }
      if (booksResponse?.Data != null && booksResponse.Data.ToString() != null)
      {
        try
        {
          books = System.Text.Json.JsonSerializer.Deserialize<List<Book>>(booksResponse.Data.ToString());
        }
        catch { }
      }
      if (copies != null && books != null)
      {
        foreach (var copy in copies)
        {
          var book = books.FirstOrDefault(b => b.Id == copy.Book?.Id);
          if (book != null && copy.Library != null &&
            (book.Title.Contains(query, StringComparison.OrdinalIgnoreCase) ||
             book.Author.Contains(query, StringComparison.OrdinalIgnoreCase)))
          {
            results.Add((copy, book, libraries.Find(l => l.Id == copy.Library.Id)));
          }
        }
      }
      return results;
    }
  }
}
