using Domain.Entities;
using Infrastructure.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Entities
{
    internal class MssqlCustomer : ISqlEntity<Customer>
    {
        protected override string TableName => "customer";

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Email { get; set; }
        public string Phone { get; set; }
        public Guid LibraryId { get; set; }

        public override string UpdateQuery => $@"update ${TableName}
set first_name={FirstName},
last_name={LastName},
email={Email ?? null},
phone={Phone},
library_id={LibraryId}
where id={Id}";

        public override string InsertQuery => $@"insert into ${TableName}
(first_name, last_name, email, phone, library_id)
values ({FirstName}, {LastName}, {Email}, {Phone}, {LibraryId})";

        public override Customer ToDomain()
        {
            return new Customer { FirstName = FirstName, LastName = LastName, Email = Email, Phone = Phone };
        }
    }
}
