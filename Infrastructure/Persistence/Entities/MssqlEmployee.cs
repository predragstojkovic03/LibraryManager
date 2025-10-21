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
set first_name={FirstName},
last_name={LastName},
password={Password},
username={Username},
library_id={LibraryId}
where id={Id}";

        public override string InsertQuery => $@"insert into {TableName}
(id, first_name, last_name, password, username, library_id)
values ({Id}, {FirstName}, {LastName}, {Password}, {Username}, {LibraryId})";


        public override Employee ToDomain()
        {
            return new Employee { FirstName = FirstName, LastName = LastName, Password = Password, Id = Id, Username = Username };
        }

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
