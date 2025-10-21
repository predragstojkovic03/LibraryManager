using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Interfaces
{
    internal abstract class ISqlEntity<T> : IEntity where T : IEntity
    {
        public Guid Id { get; }
        abstract protected string TableName { get; }
        abstract public T ToDomain();
        public abstract string UpdateQuery { get; }
        string DeleteQuery { get => $"delete from {TableName} where Id = {Id};"; }
        public abstract string InsertQuery { get; }
        public string SelectQuery { get => $"select * from {TableName};"; }
    }
}
