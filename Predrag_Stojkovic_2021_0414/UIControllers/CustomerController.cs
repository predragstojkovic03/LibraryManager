using Client.Server;
using Infrastructure.Communication;
using Server.Dtos;
using System.Text.Json;

namespace Predrag_Stojkovic_2021_0414.UIControllers
{
  public class CustomerController
  {
    private readonly ServerAdapter _serverAdapter;

    public CustomerController(ServerAdapter serverAdapter)
    {
      _serverAdapter = serverAdapter;
    }

    public List<CustomerDto> GetCustomers(Guid libraryId)
    {
      var response = _serverAdapter.MakeRequest(Operation.CustomersFindAll, null);
      if (response?.Data != null)
      {
        try
        {
          var customers = JsonSerializer.Deserialize<List<CustomerDto>>(response.Data.ToString());
          if (customers != null)
            return customers.Where(c => c.LibraryId == libraryId).ToList();
        }
        catch
        {
          // Handle or log deserialization error if needed
        }
      }
      return new List<CustomerDto>();
    }

    public List<BookCopyDto> GetBorrowedCopies(Guid customerId)
    {
      var response = _serverAdapter.MakeRequest(Operation.BookCopiesFindAll, null);
      if (response?.Data != null)
      {
        try
        {
          var copies = JsonSerializer.Deserialize<List<BookCopyDto>>(response.Data.ToString());
          if (copies != null)
            return copies.Where(bc => bc.BorrowerId == customerId).ToList();
        }
        catch
        {
          // Handle or log deserialization error if needed
        }
      }
      return new List<BookCopyDto>();
    }

    public List<BookDto> GetAllBooks(Guid libraryId)
    {
      var response = _serverAdapter.MakeRequest(Operation.BooksFindAll, null);
      if (response?.Data != null)
      {
        try
        {
          var books = JsonSerializer.Deserialize<List<BookDto>>(response.Data.ToString());
          if (books != null)
            return books.Where(b => b != null).ToList();
        }
        catch
        {
          // Handle or log deserialization error if needed
        }
      }
      return new List<BookDto>();
    }
  }
}
