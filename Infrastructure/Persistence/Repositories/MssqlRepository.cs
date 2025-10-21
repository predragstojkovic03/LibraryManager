using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence.Entities;
using Infrastructure.Persistence.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    internal class MssqlRepository<T, TPersistence> : IRepository<T> where T : IEntity where TPersistence : MssqlEntity<T>, new()
    {
        private readonly MssqlDataSource _dataSource;
        private readonly IEntityMapper<T, TPersistence> _mapper;
        public MssqlRepository(MssqlDataSource dataSource, IEntityMapper<T, TPersistence> mapper)
        {
            _dataSource = dataSource;
            _mapper = mapper;
        }

        public T Create(T entity)
        {
            var mssqlEntity = _mapper.ToPersistence(entity);
            var cmd = _dataSource.CreateCommand(null);
            cmd.CommandText = mssqlEntity.InsertQuery;
            cmd.ExecuteNonQuery();

            return entity;
        }

        public void Delete(T entity)
        {
            var mssqlEntity = _mapper.ToPersistence(entity);
            var cmd = _dataSource.CreateCommand(null);
            cmd.CommandText = mssqlEntity.DeleteQuery;
            cmd.ExecuteNonQuery();
        }

        public List<T> FindAll()
        {
            var helperEntity = new TPersistence();
            var cmd = _dataSource.CreateCommand(null);
            List<T> entities = new();
            cmd.CommandText = helperEntity.SelectQuery;

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var persistence = new TPersistence();
                    persistence.AssignFromReader(reader);
                    entities.Add(persistence.ToDomain());
                }
            }

            cmd.Dispose();
            return entities;
        }

        public T FindOne(Guid id)
        {
            var helperEntity = new TPersistence() { Id = id };
            var cmd = _dataSource.CreateCommand(null);
            cmd.CommandText = helperEntity.SelectOneQuery;

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    helperEntity.AssignFromReader(reader);
                }
            }
            cmd.Dispose();
            return helperEntity.ToDomain();
        }

        public T Update(T entity)
        {
            var mssqlEntity = _mapper.ToPersistence(entity);
            var cmd = _dataSource.CreateCommand(null);
            cmd.CommandText = mssqlEntity.UpdateQuery;
            cmd.ExecuteNonQuery();

            return entity;
        }
    }
}
