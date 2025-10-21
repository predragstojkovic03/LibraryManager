using Domain.Entities;
using Infrastructure.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Persistence.Entities
{
    internal class MssqlBookCopy : MssqlEntity<BookCopy>
    {
        public Guid LibraryId { get; set; }
        public Guid BookId { get; set; }
        public Guid BorrowerId { get; set; }
        public DateOnly PrintDate { get; set; }

        protected override string TableName => "book_copy";

        public override string UpdateQuery => $@"update {TableName}
set library_id={LibraryId},
book_id={BookId},
borrower_id={BorrowerId},
print_date={PrintDate}
where id={Id}";

        public override string InsertQuery => $@"insert into {TableName}
(id, library_id, book_id, borrower_id, print_date)
values ({Id}, {LibraryId}, {BookId}, {BorrowerId}, {PrintDate})";


        public override BookCopy ToDomain()
        {
            return new BookCopy { PrintDate = PrintDate };
        }

        public override void AssignFromReader(SqlDataReader reader)
        {
            Id = Guid.Parse(reader["id"].ToString());
            LibraryId = Guid.Parse(reader["library_id"].ToString());
            BookId = Guid.Parse(reader["book_id"].ToString());
            BorrowerId = Guid.Parse(reader["borrower_id"].ToString());
            PrintDate = DateOnly.Parse(reader["print_date"].ToString());
        }
    }
}
