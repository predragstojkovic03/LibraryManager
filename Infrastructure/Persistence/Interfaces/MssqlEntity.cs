using Domain.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Interfaces
{
    public abstract class MssqlEntity<T> : IEntity where T : IEntity
    {
        public Guid Id { get; set; }
        abstract protected string TableName { get; }
        abstract public T ToDomain();
        public abstract string UpdateQuery { get; }
        public string DeleteQuery { get => $"delete from {TableName} where Id = {Id};"; }
        public abstract string InsertQuery { get; }
        public string SelectQuery { get => $"select * from {TableName};"; }
        public string SelectOneQuery => $"select * from ${TableName} where id={Id}";
        public abstract void AssignFromReader(SqlDataReader reader);
    }
}
