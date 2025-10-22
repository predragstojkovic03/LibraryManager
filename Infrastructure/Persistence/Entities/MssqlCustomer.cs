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
    public class MssqlCustomer : MssqlEntity<Customer>
    {
        protected override string TableName => "customer";

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public Guid LibraryId { get; set; }

        public override string UpdateQuery => $@"update {TableName}
set first_name={(FirstName == null ? "NULL" : $"'{FirstName}'")},
    last_name={(LastName == null ? "NULL" : $"'{LastName}'")},
    email={(Email == null ? "NULL" : $"'{Email}'")},
    phone={(Phone == null ? "NULL" : $"'{Phone}'")},
    library_id='{LibraryId}'
where id='{Id}'";

        public override string InsertQuery => $@"insert into {TableName}
(id, first_name, last_name, email, phone, library_id)
values ('{Id}', {(FirstName == null ? "NULL" : $"'{FirstName}'")}, {(LastName == null ? "NULL" : $"'{LastName}'")}, {(Email == null ? "NULL" : $"'{Email}'")}, {(Phone == null ? "NULL" : $"'{Phone}'")}, '{LibraryId}')";

        public override void AssignFromReader(SqlDataReader reader)
        {
            Id = Guid.Parse(reader["id"].ToString());
            FirstName = reader["first_name"].ToString();
            LastName = reader["last_name"].ToString();
            Email = reader["email"].ToString();
            Phone = reader["phone"].ToString();
            LibraryId = Guid.Parse(reader["library_id"].ToString());
        }
    }
}
