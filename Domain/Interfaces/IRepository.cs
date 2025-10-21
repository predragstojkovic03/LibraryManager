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
        T FindOne(Guid id);
        List<T> FindAll();
        T Create(T entity);
        T Update(T entity);
        void Delete(Guid id);
    }
}
