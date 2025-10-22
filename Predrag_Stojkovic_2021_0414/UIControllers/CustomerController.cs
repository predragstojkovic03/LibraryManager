using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Client.Server;
using Domain.Entities;
using Infrastructure.Communication;


namespace Predrag_Stojkovic_2021_0414.UIControllers
{
  public class CustomerController
  {
    private readonly ServerAdapter _serverAdapter;

    public CustomerController(ServerAdapter serverAdapter)
    {
      _serverAdapter = serverAdapter;
    }

    public List<Customer> GetCustomers(Guid libraryId)
    {
  var response = _serverAdapter.MakeRequest(Operation.CustomersFindAll, (object?)null);
      if (response?.Data != null && response.Data.ToString() != null)
      {
        try
        {
          var customers = JsonSerializer.Deserialize<List<Customer>>(response.Data.ToString()!);
          if (customers != null)
            return customers.Where(c => c.Library != null && c.Library.Id == libraryId).ToList();
        }
        catch
        {
          // Handle or log deserialization error if needed
        }
      }
      return new List<Customer>();
    }

    public List<BookCopy> GetBorrowedCopies(Guid customerId)
    {
     var response = _serverAdapter.MakeRequest(Operation.BookCopiesFindAll, (object?)null);
      if (response?.Data != null && response.Data.ToString() != null)
      {
        try
        {
          var copies = JsonSerializer.Deserialize<List<BookCopy>>(response.Data.ToString()!);
          if (copies != null)
            return copies.Where(bc => bc.Borrower != null && bc.Borrower.Id == customerId).ToList();
        }
        catch
        {
          // Handle or log deserialization error if needed
        }
      }
      return new List<BookCopy>();
    }

    public List<BookCopy> GetAllBookCopies(Guid libraryId)
    {
  var response = _serverAdapter.MakeRequest(Operation.BookCopiesFindAll, (object?)null);
      if (response?.Data != null && response.Data.ToString() != null)
      {
        try
        {
          var copies = JsonSerializer.Deserialize<List<BookCopy>>(response.Data.ToString()!);
          if (copies != null)
            return copies.Where(bc => bc.Library != null && bc.Library.Id == libraryId).ToList();
        }
        catch
        {
          // Handle or log deserialization error if needed
        }
      }
      return new List<BookCopy>();
    }

  public List<Book> GetAllBooks(Guid libraryId)
    {
      var response = _serverAdapter.MakeRequest(Operation.BooksFindAll, null);
      if (response?.Data != null && response.Data.ToString() != null)
      {
        try
        {
          var books = JsonSerializer.Deserialize<List<Book>>(response.Data.ToString()!);
          if (books != null)
            return books.Where(b => b != null).ToList();
        }
        catch
        {
          // Handle or log deserialization error if needed
        }
      }
      return new List<Book>();
    }

    public void BorrowBookCopy(Guid customerId, Guid copyId, Guid libraryId)
    {
      // Use BookCopiesUpdate: set BorrowerId to customerId for the selected copy in the correct library
      var copy = GetAllBookCopies(libraryId).FirstOrDefault(bc => bc.Id == copyId);
      if (copy != null)
      {
        copy.Borrower = new Customer { Id = customerId };
        _serverAdapter.MakeRequest(Operation.BookCopiesUpdate, copy);
      }
    }

    public void ReturnBookCopy(Guid copyId, Guid libraryId)
    {
      // Use BookCopiesUpdate: set BorrowerId to Guid.Empty for the selected copy in the correct library
      var copy = GetAllBookCopies(libraryId).FirstOrDefault(bc => bc.Id == copyId);
      if (copy != null)
      {
  copy.Borrower = default;
        _serverAdapter.MakeRequest(Operation.BookCopiesUpdate, copy);
      }
    }
  }
}
