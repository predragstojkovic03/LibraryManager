using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Customer : IEntity
    {
        public Guid Id { get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Email { get; set; }
        public string Phone { get; set; }
        public List<BookCopy> BorrowedCopies { get; set; }
        public Library library { get; set; }
        public string FullName { get => $"{FirstName} ${LastName}"; }
    }
}
