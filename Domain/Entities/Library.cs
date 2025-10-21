using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Library : IEntity
    {
        public Guid Id { get; }
        public string Name { get; set; }
        public string Address { get; set; }
        public List<BookCopy> BookCopies { get; set; }
    }
}
