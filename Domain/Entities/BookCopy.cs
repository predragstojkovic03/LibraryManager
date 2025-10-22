using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BookCopy : IEntity
    {
        public Guid Id { get; set; }
        public Library Library { get; set; }
        public Book Book { get; set; }
    public Customer? Borrower { get; set; }
        public DateTime PrintDate { get; set; }
    }
}
