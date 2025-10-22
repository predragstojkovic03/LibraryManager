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
    public class MssqlBookCopy : MssqlEntity<BookCopy>
    {
        public Guid LibraryId { get; set; }
        public Guid BookId { get; set; }
        public Guid? BorrowerId { get; set; }
        public DateTime PrintDate { get; set; }

        protected override string TableName => "book_copy";

        public override string UpdateQuery => $@"update {TableName}
set library_id='{LibraryId}',
    book_id='{BookId}',
    borrower_id={(BorrowerId == null ? "NULL" : $"'{BorrowerId}'")},
    print_date='{PrintDate:yyyy-MM-dd HH:mm:ss}'
where id='{Id}'";

        public override string InsertQuery => $@"insert into {TableName}
(id, library_id, book_id, borrower_id, print_date)
values ('{Id}', '{LibraryId}', '{BookId}', {(BorrowerId == null ? "NULL" : $"'{BorrowerId}'")}, '{PrintDate:yyyy-MM-dd HH:mm:ss}')";


        public override void AssignFromReader(SqlDataReader reader)
        {
            Id = Guid.Parse(reader["id"].ToString());
            LibraryId = Guid.Parse(reader["library_id"].ToString());
            BookId = Guid.Parse(reader["book_id"].ToString());
            BorrowerId = reader["borrower_id"] != DBNull.Value ? Guid.Parse(reader["borrower_id"].ToString()) : (Guid?)null;
            PrintDate = DateTime.Parse(reader["print_date"].ToString());
        }
    }
}
