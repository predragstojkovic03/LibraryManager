using Azure.Identity;
using Domain.Entities;
using Infrastructure.Persistence.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Entities
{
    public class MssqlLibrary : MssqlEntity<Library>
    {
        protected override string TableName => "library";

        public string Address { get; set; }
        public string Name { get; set; }

        public override string UpdateQuery => $"update {TableName} set address={(Address == null ? "NULL" : $"'{Address}'")}, name={(Name == null ? "NULL" : $"'{Name}'")} where id='{Id}';";
        public override string InsertQuery => $"insert into library(id, name, address) values ('{Id}', {(Name == null ? "NULL" : $"'{Name}'")}, {(Address == null ? "NULL" : $"'{Address}'")});";

        public static MssqlLibrary ToPersistence(Library library)
        {
            return new MssqlLibrary { Address = library.Address, Name = library.Name };
        }

        public override void AssignFromReader(SqlDataReader reader)
        {
            Id = Guid.Parse(reader["id"].ToString());
            Address = reader["address"].ToString();
            Name = reader["name"].ToString();
        }
    }
}
