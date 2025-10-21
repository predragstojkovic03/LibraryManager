using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Application.Services;
using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Entities;
using Infrastructure.Persistence.Mappers;
using Infrastructure.Persistence.Repositories;
using Server.Controllers;

namespace Server
{
    internal class Server
    {
        private readonly Socket _serverSocket =
            new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private readonly List<ClientHandler> _clients = new();
        private object _lock = new object();

        // Example controller instances
        private EmployeeController _employeeController;
        private CustomerController _customerController;
        private BookController _bookController;
        private BookCopyController _bookCopyController;
        private LibraryController _libraryController;

        public Server()
        {
            _employeeController = new EmployeeController(new CrudService<Employee>(new MssqlRepository<Employee, MssqlEmployee>(new MssqlDataSource(), new MssqlEmployeeMapper())));
            _customerController = new CustomerController(new CrudService<Customer>(new MssqlRepository<Customer, MssqlCustomer>(new MssqlDataSource(), new MssqlCustomerMapper())));
            _bookController = new BookController(new CrudService<Book>(new MssqlRepository<Book, MssqlBook>(new MssqlDataSource(), new MssqlBookMapper())));
            _bookCopyController = new BookCopyController(new CrudService<BookCopy>(new MssqlRepository<BookCopy, MssqlBookCopy>(new MssqlDataSource(), new MssqlBookCopyMapper())));
            _libraryController = new LibraryController(new CrudService<Library>(new MssqlRepository<Library, MssqlLibrary>(new MssqlDataSource(), new MssqlLibraryMapper())));
        }

        public T GetController<T>() where T : class
        {
            if (typeof(T) == typeof(EmployeeController)) return _employeeController as T;
            if (typeof(T) == typeof(CustomerController)) return _customerController as T;
            if (typeof(T) == typeof(BookController)) return _bookController as T;
            if (typeof(T) == typeof(BookCopyController)) return _bookCopyController as T;
            if (typeof(T) == typeof(LibraryController)) return _libraryController as T;
            throw new InvalidOperationException($"Controller of type {typeof(T).Name} not found.");
        }

        public void Listen(int port)
        {
            try
            {
                _serverSocket.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), port));
                _serverSocket.Listen(100);
                Console.WriteLine($"Server listening on port {port}...");

                while (true)
                {
                    var clientSocket = _serverSocket.Accept();
                    Console.WriteLine($"Client connected: {clientSocket.RemoteEndPoint}");
                    ClientHandler handler = new(clientSocket, this);
                    _clients.Add(handler);
                    Thread handlerThread = new(handler.Handle) { IsBackground = true };
                    handlerThread.Start();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Server error: {ex.Message}");
            }
        }

        public void RemoveClient(ClientHandler handler)
        {
            lock (_lock)
            {
                _clients.Remove(handler);
            }
        }
    }
}
