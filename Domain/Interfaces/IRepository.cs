using Microsoft.Data.SqlClient;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepository<T> where T : IEntity
    {
        T FindOne(Guid id, SqlTransaction transaction = null);
        List<T> FindAll();
        T Create(T entity, Microsoft.Data.SqlClient.SqlTransaction transaction = null);
        T Update(T entity, Microsoft.Data.SqlClient.SqlTransaction transaction = null);
        void Delete(T entity);
    }
}
