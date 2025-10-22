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
            try
            {
                var mssqlEntity = _mapper.ToPersistence(entity);
                var cmd = _dataSource.CreateCommand(null);
                _dataSource.OpenConnection();
                cmd.CommandText = mssqlEntity.InsertQuery;
                cmd.ExecuteNonQuery();
                _dataSource.CloseConnection();

                return entity;
            }
            catch (SqlException ex)
            {
                _dataSource.CloseConnection();
                Console.WriteLine($"SQL Error {ex.Number}: {ex.Message}");
                throw;
            }
        }

        public void Delete(T entity)
        {
            try
            {
                var mssqlEntity = _mapper.ToPersistence(entity);
                var cmd = _dataSource.CreateCommand(null);
                cmd.CommandText = mssqlEntity.DeleteQuery;

                _dataSource.OpenConnection();
                cmd.ExecuteNonQuery();
                _dataSource.CloseConnection();
            }
            catch (SqlException ex)
            {
                _dataSource.CloseConnection();
                Console.WriteLine($"SQL Error {ex.Number}: {ex.Message}");
                throw;
            }
        }

        public List<T> FindAll()
        {
            try
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
                        entities.Add(_mapper.ToDomain(persistence));
                    }
                }
                _dataSource.CloseConnection();

                cmd.Dispose();
                return entities;
            }
            catch (SqlException ex)
            {
                _dataSource.CloseConnection();
                Console.WriteLine($"SQL Error {ex.Number}: {ex.Message}");
                throw;
            }
        }

        public T FindOne(Guid id)
        {
            try
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
                return _mapper.ToDomain(helperEntity);
            }
            catch (SqlException ex)
            {
                _dataSource.CloseConnection();
                Console.WriteLine($"SQL Error {ex.Number}: {ex.Message}");
                throw;
            }
        }

        public T Update(T entity)
        {
            try
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
            catch (SqlException ex)
            {
                _dataSource.CloseConnection();
                Console.WriteLine($"SQL Error {ex.Number}: {ex.Message}");
                throw;
            }
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
                // For reference types, allow explicit null to overwrite
                if (!prop.PropertyType.IsValueType)
                {
                    prop.SetValue(target, sourceValue);
                }
                // For value types, only update if not default
                else if (sourceValue != null && !sourceValue.Equals(defaultValue))
                {
                    prop.SetValue(target, sourceValue);
                }
            }
            return target;
        }
    }
}
