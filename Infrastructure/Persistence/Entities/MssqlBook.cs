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
    public class MssqlBook : MssqlEntity<Book>
    {
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Description { get; set; }

        protected override string TableName => "book";

        public override string UpdateQuery => $@"update {TableName}
set title={(Title == null ? "NULL" : $"'{Title}'")},
    author={(Author == null ? "NULL" : $"'{Author}'")},
    description={(Description == null ? "NULL" : $"'{Description}'")}
where id='{Id}'";

        public override string InsertQuery => $@"insert into {TableName}
(title, author, description)
values ({(Title == null ? "NULL" : $"'{Title}'")}, {(Author == null ? "NULL" : $"'{Author}'")}, {(Description == null ? "NULL" : $"'{Description}'")})";


        public override void AssignFromReader(SqlDataReader reader)
        {
            Id = Guid.Parse(reader["id"].ToString());
            Title = reader["title"].ToString();
            Author = reader["author"].ToString();
            Description = reader["description"].ToString();
        }
    }
}
