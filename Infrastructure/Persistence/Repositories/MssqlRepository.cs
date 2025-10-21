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
using Domain.Exceptions;

namespace Infrastructure.Persistence.Repositories
{
    public class MssqlRepository<T, TPersistence> : IRepository<T> where T : IEntity where TPersistence : MssqlEntity<T>, new()
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
            _dataSource.OpenConnection();
            cmd.CommandText = mssqlEntity.InsertQuery;
            cmd.ExecuteNonQuery();
            _dataSource.CloseConnection();

            return entity;
        }

        public void Delete(T entity)
        {
            var mssqlEntity = _mapper.ToPersistence(entity);
            var cmd = _dataSource.CreateCommand(null);
            cmd.CommandText = mssqlEntity.DeleteQuery;

            _dataSource.OpenConnection();
            cmd.ExecuteNonQuery();
            _dataSource.CloseConnection();
        }

        public List<T> FindAll()
        {
            var helperEntity = new TPersistence();
            var cmd = _dataSource.CreateCommand(null);
            List<T> entities = new();
            cmd.CommandText = helperEntity.SelectQuery;

            _dataSource.OpenConnection();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var persistence = new TPersistence();
                    persistence.AssignFromReader(reader);
                    entities.Add(persistence.ToDomain());
                }
            }
            _dataSource.CloseConnection();

            cmd.Dispose();
            return entities;
        }

        public T FindOne(Guid id)
        {
            var helperEntity = new TPersistence() { Id = id };
            var cmd = _dataSource.CreateCommand(null);
            cmd.CommandText = helperEntity.SelectOneQuery;

            _dataSource.OpenConnection();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    helperEntity.AssignFromReader(reader);
                }
            }

            _dataSource.CloseConnection();
            cmd.Dispose();
            return helperEntity.ToDomain();
        }

        public T Update(T entity)
        {
            // Check if entity exists
            var existing = FindOne(entity.Id);
            if (existing == null)
            {
                throw new EntityNotFoundException($"Entity with id {entity.Id} not found.");
            }

            // Merge properties for partial update
            var mergedEntity = MergeEntities(existing, entity);
            var mssqlEntity = _mapper.ToPersistence(mergedEntity);
            var cmd = _dataSource.CreateCommand(null);
            cmd.CommandText = mssqlEntity.UpdateQuery;

            _dataSource.OpenConnection();
            cmd.ExecuteNonQuery();
            _dataSource.CloseConnection();

            return mergedEntity;
        }

        /// <summary>
        /// Merges non-default properties from source into target for partial update.
        /// </summary>
        private static T MergeEntities(T target, T source)
        {
            var type = typeof(T);
            foreach (var prop in type.GetProperties())
            {
                var sourceValue = prop.GetValue(source);
                var defaultValue = prop.PropertyType.IsValueType ? Activator.CreateInstance(prop.PropertyType) : null;
                // Only update if sourceValue is not default/null
                if (sourceValue != null && !sourceValue.Equals(defaultValue))
                {
                    prop.SetValue(target, sourceValue);
                }
            }
            return target;
        }
    }
}
