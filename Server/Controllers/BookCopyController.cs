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

        public BookCopy UpdateBookCopy(BookCopy bookCopyDto)
        {
            var dataSource = new Infrastructure.Persistence.MssqlDataSource();
            dataSource.OpenConnection();
            var transaction = dataSource.Connection.BeginTransaction();
            var bookCopyRepo = new Infrastructure.Persistence.Repositories.MssqlRepository<BookCopy, Infrastructure.Persistence.Entities.MssqlBookCopy>(dataSource, new Infrastructure.Persistence.Mappers.MssqlBookCopyMapper());
            var logRepo = new Infrastructure.Persistence.Repositories.MssqlRepository<Log, Infrastructure.Persistence.Entities.MssqlLog>(dataSource, new Infrastructure.Persistence.Mappers.MssqlLogMapper());
            var bookCopyService = new Application.Services.CrudService<BookCopy>(bookCopyRepo);
            var logService = new Application.Services.CrudService<Log>(logRepo);
            try
            {
                var bookCopy = new BookCopy
                {
                    Id = bookCopyDto.Id,
                    Library = new Library { Id = bookCopyDto.Library.Id },
                    Book = new Book { Id = bookCopyDto.Book.Id },
                    Borrower = bookCopyDto.Borrower != null ? new Customer { Id = bookCopyDto.Borrower.Id } : null,
                    PrintDate = bookCopyDto.PrintDate
                };
                var updatedCopy = bookCopyService.Update(bookCopy, transaction);

                var isBorrowed = bookCopyDto.Borrower != null;
                var log = new Log
                {
                    Id = Guid.NewGuid(),
                    EventType = isBorrowed ? "Borrow" : "Return",
                    Description = isBorrowed ? $"BookCopy {bookCopy.Id} borrowed by Customer {bookCopy.Borrower?.Id}" : $"BookCopy {bookCopy.Id} returned.",
                    Timestamp = DateTime.Now,
                    BookCopyId = bookCopy.Id,
                    CustomerId = bookCopy.Borrower?.Id
                };
                logService.Create(log, transaction);
                transaction.Commit();
                return updatedCopy;
            }
            catch
            {
                transaction.Rollback();
                dataSource.CloseConnection();
                throw;
            }
        }
    }
}
