using Infrastructure.Communication;
using Infrastructure.Dto;
using Server.Controllers;
using Server.Dtos;
using System.Net.Sockets;

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
            var req = _adapter.Receive<Request>();
            try
            {
                var res = HandleRequest(req);
                _adapter.Send(res);
            }
            catch (Exception ex)
            {
                _adapter.Send(new Response { Message = ex.Message, Status = 1 });
            }
        }

        private Response HandleRequest(Request request)
        {
            switch (request.Operation)
            {
                case Operation.AuthLogin:
                    {
                        var controller = _server.GetController<EmployeeController>();
                        var dto = request.Payload as AuthLoginDto;
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
                        var id = (Guid)request.Payload;
                        var book = controller.GetBookById(id);
                        return new Response { Data = book, Status = 0 };
                    }
                case Operation.BooksCreate:
                    {
                        var controller = _server.GetController<BookController>();
                        var dto = request.Payload as BookDto;
                        var book = controller.CreateBook(dto);
                        return new Response { Data = book, Status = 0 };
                    }
                case Operation.BooksUpdate:
                    {
                        var controller = _server.GetController<BookController>();
                        var dto = request.Payload as BookDto;
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
                        var id = (Guid)request.Payload;
                        var copy = controller.GetBookCopyById(id);
                        return new Response { Data = copy, Status = 0 };
                    }
                case Operation.BookCopiesCreate:
                    {
                        var controller = _server.GetController<BookCopyController>();
                        var dto = request.Payload as BookCopyDto;
                        var copy = controller.CreateBookCopy(dto);
                        return new Response { Data = copy, Status = 0 };
                    }
                case Operation.BookCopiesUpdate:
                    {
                        var controller = _server.GetController<BookCopyController>();
                        var dto = request.Payload as BookCopyDto;
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
                        var id = (Guid)request.Payload;
                        var customer = controller.GetCustomerById(id);
                        return new Response { Data = customer, Status = 0 };
                    }
                case Operation.CustomersCreate:
                    {
                        var controller = _server.GetController<CustomerController>();
                        var dto = request.Payload as CustomerDto;
                        var customer = controller.CreateCustomer(dto);
                        return new Response { Data = customer, Status = 0 };
                    }
                case Operation.CustomersUpdate:
                    {
                        var controller = _server.GetController<CustomerController>();
                        var dto = request.Payload as CustomerDto;
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
                        var id = (Guid)request.Payload;
                        var employee = controller.GetEmployeeById(id);
                        return new Response { Data = employee, Status = 0 };
                    }
                case Operation.EmployeesCreate:
                    {
                        var controller = _server.GetController<EmployeeController>();
                        var dto = request.Payload as EmployeeDto;
                        var employee = controller.CreateEmployee(dto);
                        return new Response { Data = employee, Status = 0 };
                    }
                case Operation.EmployeesUpdate:
                    {
                        var controller = _server.GetController<EmployeeController>();
                        var dto = request.Payload as EmployeeDto;
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
                        var id = (Guid)request.Payload;
                        var library = controller.GetLibraryById(id);
                        return new Response { Data = library, Status = 0 };
                    }
                case Operation.LibrariesCreate:
                    {
                        var controller = _server.GetController<LibraryController>();
                        var dto = request.Payload as LibraryDto;
                        var library = controller.CreateLibrary(dto);
                        return new Response { Data = library, Status = 0 };
                    }
                case Operation.LibrariesUpdate:
                    {
                        var controller = _server.GetController<LibraryController>();
                        var dto = request.Payload as LibraryDto;
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
