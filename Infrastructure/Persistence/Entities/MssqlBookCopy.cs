using Domain.Entities;
using Infrastructure.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Entities
{
    internal class MssqlBookCopy : ISqlEntity<BookCopy>
    {
        public Guid LibraryId { get; set; }
        public Guid BookId { get; set; }
        public Guid Borrower {  get; set; }
        public DateOnly PrintDate { get; set; }

        protected override string TableName => "book_copy";

        public override string UpdateQuery => throw new NotImplementedException();

        public override string InsertQuery => throw new NotImplementedException();


        public override BookCopy ToDomain()
        {
            throw new NotImplementedException();
        }
    }
}
