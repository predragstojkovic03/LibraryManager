using Azure.Identity;
using Domain.Entities;
using Infrastructure.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Entities
{
    internal class MssqlLibrary : ISqlEntity<Library>
    {
        protected override string TableName => "library";

        public string Address { get; set; }
        public string Name { get; set; }

        public override string UpdateQuery => $"update {TableName} set address={Address}, name={Name} where id={Id};";
        public override string InsertQuery => $"insert into library(name,address) values ({Name}, {Address});";

        public override Library ToDomain()
        {
            return new Library { Address = Address, Name = Name };
        }

        public static MssqlLibrary ToPersistence(Library library)
        {
            return new MssqlLibrary { Address = library.Address, Name = library.Name }
        }
    }
}
