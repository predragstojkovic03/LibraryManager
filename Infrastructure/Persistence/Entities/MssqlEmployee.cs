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
    public class MssqlEmployee : MssqlEntity<Employee>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Password { get; set; }
        public string? Username { get; set; }
        public Guid LibraryId { get; set; }

        protected override string TableName => "employee";
        public override string UpdateQuery => $@"update {TableName}
set first_name={(FirstName == null ? "NULL" : $"'{FirstName}'")},
    last_name={(LastName == null ? "NULL" : $"'{LastName}'")},
    password={(Password == null ? "NULL" : $"'{Password}'")},
    username={(Username == null ? "NULL" : $"'{Username}'")},
    library_id='{LibraryId}'
where id='{Id}'";

        public override string InsertQuery => $@"insert into {TableName}
(id, first_name, last_name, password, username, library_id)
values ('{Id}', {(FirstName == null ? "NULL" : $"'{FirstName}'")}, {(LastName == null ? "NULL" : $"'{LastName}'")}, {(Password == null ? "NULL" : $"'{Password}'")}, {(Username == null ? "NULL" : $"'{Username}'")}, '{LibraryId}')";



        public override void AssignFromReader(SqlDataReader reader)
        {
            Id = Guid.Parse(reader["id"].ToString());
            FirstName = reader["first_name"].ToString();
            LastName = reader["last_name"].ToString();
            Password = reader["password"].ToString();
            Username = reader["username"].ToString();
            LibraryId = Guid.Parse(reader["library_id"].ToString());
        }
    }
}
