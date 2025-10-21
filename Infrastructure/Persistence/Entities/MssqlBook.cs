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
set title={Title},
author={Author},
description={Description}
where id={Id}";

        public override string InsertQuery => $@"insert into {TableName}
(title, author, description)
values ({Title}, {Author}, {Description})";


        public override Book ToDomain()
        {
            return new Book { Title = Title, Author = Author, Description = Description };
        }

        public override void AssignFromReader(SqlDataReader reader)
        {
            Id = Guid.Parse(reader["id"].ToString());
            Title = reader["title"].ToString();
            Author = reader["author"].ToString();
            Description = reader["description"].ToString();
        }
    }
}
