using Domain.Entities;
using Infrastructure.Communication;
using Infrastructure.Dto;
using Server.Controllers;
using Server.Dtos;
using System.Net.Sockets;
using System.Text.Json;

namespace Server
{
    internal class ClientHandler
    {
        private Socket _socket;
        private Server _server;
        private CommunicationAdapter _adapter;

        public ClientHandler(Socket clientSocket, Server server)
        {
            _socket = clientSocket;
            _server = server;
            _adapter = new(_socket);
        }

        public void Handle()
        {
            while (true)
            {
                try
                {
                    var req = _adapter.Receive<Request>();
                    if (req == null)
                    {
                        // Client disconnected or sent invalid data
                        break;
                    }
                    Console.WriteLine("Recieved request");
                    var res = HandleRequest(req);
                    _adapter.Send(res);
                }
                catch (Exception ex)
                {
                    _adapter.Send(new Response { Message = ex.Message, Status = 1 });
                }
            }
        }

        private Response HandleRequest(Request request)
        {
            switch (request.Operation)
            {
                case Operation.LogCreate:
                    {
                        var controller = _server.GetController<LogController>();
                        if (request.Payload == null)
                            return new Response { Status = 1, Message = "Log data is missing or invalid." };
                        var log = JsonSerializer.Deserialize<Log>(request.Payload.ToString());
                        if (log == null)
                            return new Response { Status = 1, Message = "Log data is missing or invalid." };
                        var createdLog = controller.CreateLog(log);
                        return new Response { Data = createdLog, Status = 0, Message = "Log saved successfully." };
                    }
                case Operation.AuthLogin:
                    {
                        var controller = _server.GetController<EmployeeController>();
                        if (request.Payload == null)
                            return new Response { Status = 1, Message = "Login data is missing or invalid." };
                        var dto = JsonSerializer.Deserialize<AuthLoginDto>(request.Payload.ToString());
                        if (dto == null)
                            return new Response { Status = 1, Message = "Login data is missing or invalid." };
                        var employee = controller.GetEmployeeList().Find(e => e.Username == dto.Username && e.Password == dto.Password);
                        if (employee == null)
                            return new Response { Status = 1, Message = "Invalid username or password" };
                        return new Response { Data = employee, Status = 0, Message = "Login successful" };
                    }
                case Operation.BooksFindAll:
                    {
                        var controller = _server.GetController<BookController>();
                        var books = controller.GetBookList();
                        return new Response { Data = books, Status = 0 };
                    }
                case Operation.BooksFindOne:
                    {
                        var controller = _server.GetController<BookController>();
                        if (request.Payload == null)
                            return new Response { Status = 1, Message = "Book id is missing." };
                        Guid bookId;
                        try
                        {
                            bookId = JsonSerializer.Deserialize<Guid>(request.Payload.ToString());
                        }
                        catch
                        {
                            return new Response { Status = 1, Message = "Invalid book id." };
                        }
                        var book = controller.GetBookById(bookId);
                        return new Response { Data = book, Status = 0 };
                    }
                case Operation.BooksCreate:
                    {
                        var controller = _server.GetController<BookController>();
                        if (request.Payload == null)
                            return new Response { Status = 1, Message = "Book data is missing or invalid." };
                        var dto = JsonSerializer.Deserialize<BookDto>(request.Payload.ToString());
                        if (dto == null)
                            return new Response { Status = 1, Message = "Book data is missing or invalid." };
                        var book = controller.CreateBook(dto);
                        return new Response { Data = book, Status = 0 };
                    }
                case Operation.BooksUpdate:
                    {
                        var controller = _server.GetController<BookController>();
                        if (request.Payload == null)
                            return new Response { Status = 1, Message = "Book data is missing or invalid." };
                        var dto = JsonSerializer.Deserialize<BookDto>(request.Payload.ToString());
                        if (dto == null)
                            return new Response { Status = 1, Message = "Book data is missing or invalid." };
                        var book = controller.UpdateBook(dto);
                        return new Response { Data = book, Status = 0 };
                    }
                case Operation.BooksDelete:
                    {
                        // Implement delete logic as needed
                        return new Response { Status = 0, Message = "Book deleted" };
                    }
                case Operation.BookCopiesFindAll:
                    {
                        var controller = _server.GetController<BookCopyController>();
                        var copies = controller.GetBookCopyList();
                        return new Response { Data = copies, Status = 0 };
                    }
                case Operation.BookCopiesFindOne:
                    {
                        var controller = _server.GetController<BookCopyController>();
                        if (request.Payload == null)
                            return new Response { Status = 1, Message = "Book copy id is missing." };
                        Guid copyId;
                        try
                        {
                            copyId = JsonSerializer.Deserialize<Guid>(request.Payload.ToString());
                        }
                        catch
                        {
                            return new Response { Status = 1, Message = "Invalid book copy id." };
                        }
                        var copy = controller.GetBookCopyById(copyId);
                        return new Response { Data = copy, Status = 0 };
                    }
                case Operation.BookCopiesCreate:
                    {
                        var controller = _server.GetController<BookCopyController>();
                        if (request.Payload == null)
                            return new Response { Status = 1, Message = "BookCopy data is missing or invalid." };
                        var dto = JsonSerializer.Deserialize<BookCopyDto>(request.Payload.ToString());
                        if (dto == null)
                            return new Response { Status = 1, Message = "BookCopy data is missing or invalid." };
                        var copy = controller.CreateBookCopy(dto);
                        return new Response { Data = copy, Status = 0 };
                    }
                case Operation.BookCopiesUpdate:
                    {
                        var controller = _server.GetController<BookCopyController>();
                        if (request.Payload == null)
                            return new Response { Status = 1, Message = "BookCopy data is missing or invalid." };
                        var dto = JsonSerializer.Deserialize<BookCopy>(request.Payload.ToString());
                        if (dto == null)
                            return new Response { Status = 1, Message = "BookCopy data is missing or invalid." };
                        var copy = controller.UpdateBookCopy(dto);
                        return new Response { Data = copy, Status = 0 };
                    }
                case Operation.BookCopiesDelete:
                    {
                        // Implement delete logic as needed
                        return new Response { Status = 0, Message = "BookCopy deleted" };
                    }
                case Operation.CustomersFindAll:
                    {
                        var controller = _server.GetController<CustomerController>();
                        var customers = controller.GetCustomerList();
                        return new Response { Data = customers, Status = 0 };
                    }
                case Operation.CustomersFindOne:
                    {
                        var controller = _server.GetController<CustomerController>();
                        if (request.Payload == null)
                            return new Response { Status = 1, Message = "Customer id is missing." };
                        Guid customerId;
                        try
                        {
                            customerId = JsonSerializer.Deserialize<Guid>(request.Payload.ToString());
                        }
                        catch
                        {
                            return new Response { Status = 1, Message = "Invalid customer id." };
                        }
                        var customer = controller.GetCustomerById(customerId);
                        return new Response { Data = customer, Status = 0 };
                    }
                case Operation.CustomersCreate:
                    {
                        var controller = _server.GetController<CustomerController>();
                        if (request.Payload == null)
                            return new Response { Status = 1, Message = "Customer data is missing or invalid." };
                        var dto = JsonSerializer.Deserialize<CustomerDto>(request.Payload.ToString());
                        if (dto == null)
                            return new Response { Status = 1, Message = "Customer data is missing or invalid." };
                        var customer = controller.CreateCustomer(dto);
                        return new Response { Data = customer, Status = 0 };
                    }
                case Operation.CustomersUpdate:
                    {
                        var controller = _server.GetController<CustomerController>();
                        if (request.Payload == null)
                            return new Response { Status = 1, Message = "Customer data is missing or invalid." };
                        var dto = JsonSerializer.Deserialize<CustomerDto>(request.Payload.ToString());
                        if (dto == null)
                            return new Response { Status = 1, Message = "Customer data is missing or invalid." };
                        var customer = controller.UpdateCustomer(dto);
                        return new Response { Data = customer, Status = 0 };
                    }
                case Operation.CustomersDelete:
                    {
                        // Implement delete logic as needed
                        return new Response { Status = 0, Message = "Customer deleted" };
                    }
                case Operation.EmployeesFindAll:
                    {
                        var controller = _server.GetController<EmployeeController>();
                        var employees = controller.GetEmployeeList();
                        return new Response { Data = employees, Status = 0 };
                    }
                case Operation.EmployeesFindOne:
                    {
                        var controller = _server.GetController<EmployeeController>();
                        if (request.Payload == null)
                            return new Response { Status = 1, Message = "Employee id is missing." };
                        Guid employeeId;
                        try
                        {
                            employeeId = JsonSerializer.Deserialize<Guid>(request.Payload.ToString());
                        }
                        catch
                        {
                            return new Response { Status = 1, Message = "Invalid employee id." };
                        }
                        var employee = controller.GetEmployeeById(employeeId);
                        return new Response { Data = employee, Status = 0 };
                    }
                case Operation.EmployeesCreate:
                    {
                        var controller = _server.GetController<EmployeeController>();
                        if (request.Payload == null)
                            return new Response { Status = 1, Message = "Employee data is missing or invalid." };
                        var dto = JsonSerializer.Deserialize<EmployeeDto>(request.Payload.ToString());
                        if (dto == null)
                            return new Response { Status = 1, Message = "Employee data is missing or invalid." };
                        var employee = controller.CreateEmployee(dto);
                        return new Response { Data = employee, Status = 0 };
                    }
                case Operation.EmployeesUpdate:
                    {
                        var controller = _server.GetController<EmployeeController>();
                        if (request.Payload == null)
                            return new Response { Status = 1, Message = "Employee data is missing or invalid." };
                        var dto = JsonSerializer.Deserialize<EmployeeDto>(request.Payload.ToString());
                        if (dto == null)
                            return new Response { Status = 1, Message = "Employee data is missing or invalid." };
                        var employee = controller.UpdateEmployee(dto);
                        return new Response { Data = employee, Status = 0 };
                    }
                case Operation.EmployeesDelete:
                    {
                        // Implement delete logic as needed
                        return new Response { Status = 0, Message = "Employee deleted" };
                    }
                case Operation.LibrariesFindAll:
                    {
                        var controller = _server.GetController<LibraryController>();
                        var libraries = controller.GetLibraryList();
                        return new Response { Data = libraries, Status = 0 };
                    }
                case Operation.LibrariesFindOne:
                    {
                        var controller = _server.GetController<LibraryController>();
                        if (request.Payload == null)
                            return new Response { Status = 1, Message = "Library id is missing." };
                        Guid libraryId;
                        try
                        {
                            libraryId = JsonSerializer.Deserialize<Guid>(request.Payload.ToString());
                        }
                        catch
                        {
                            return new Response { Status = 1, Message = "Invalid library id." };
                        }
                        var library = controller.GetLibraryById(libraryId);
                        return new Response { Data = library, Status = 0 };
                    }
                case Operation.LibrariesCreate:
                    {
                        var controller = _server.GetController<LibraryController>();
                        if (request.Payload == null)
                            return new Response { Status = 1, Message = "Library data is missing or invalid." };
                        var dto = JsonSerializer.Deserialize<LibraryDto>(request.Payload.ToString());
                        if (dto == null)
                            return new Response { Status = 1, Message = "Library data is missing or invalid." };
                        var library = controller.CreateLibrary(dto);
                        return new Response { Data = library, Status = 0 };
                    }
                case Operation.LibrariesUpdate:
                    {
                        var controller = _server.GetController<LibraryController>();
                        if (request.Payload == null)
                            return new Response { Status = 1, Message = "Library data is missing or invalid." };
                        var dto = JsonSerializer.Deserialize<LibraryDto>(request.Payload.ToString());
                        if (dto == null)
                            return new Response { Status = 1, Message = "Library data is missing or invalid." };
                        var library = controller.UpdateLibrary(dto);
                        return new Response { Data = library, Status = 0 };
                    }
                case Operation.LibrariesDelete:
                    {
                        // Implement delete logic as needed
                        return new Response { Status = 0, Message = "Library deleted" };
                    }
                default:
                    return new Response { Status = 1, Message = "Unknown operation" };
            }
        }
    }
}
