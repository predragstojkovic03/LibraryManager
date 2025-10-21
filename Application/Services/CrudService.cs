using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CrudService<T> where T : IEntity
    {
        private readonly IRepository<T> _repository;

        public CrudService(IRepository<T> repository)
        {
            _repository = repository;
        }

        public T GetById(Guid id)
        {
            return _repository.FindOne(id);
        }

        public List<T> GetAll()
        {
            return _repository.FindAll();
        }

        public T Create(T entity)
        {
            return _repository.Create(entity);
        }

        public T Update(T entity)
        {
            return _repository.Update(entity);
        }
    }
}