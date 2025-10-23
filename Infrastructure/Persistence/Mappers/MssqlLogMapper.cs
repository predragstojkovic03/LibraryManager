using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence.Entities;
using System;

namespace Infrastructure.Persistence.Mappers
{
    public class MssqlLogMapper : IEntityMapper<Log, MssqlLog>
    {
        public Log ToDomain(MssqlLog persistence)
        {
            return new Log
            {
                Id = persistence.Id,
                Timestamp = persistence.Timestamp,
                EventType = persistence.EventType ?? string.Empty,
                Description = persistence.Description ?? string.Empty,
                BookCopyId = persistence.BookCopyId,
                CustomerId = persistence.CustomerId,
                EmployeeId = persistence.EmployeeId
            };
        }

        public MssqlLog ToPersistence(Log domain)
        {
            return new MssqlLog
            {
                Id = domain.Id,
                Timestamp = domain.Timestamp,
                EventType = domain.EventType,
                Description = domain.Description,
                BookCopyId = domain.BookCopyId,
                CustomerId = domain.CustomerId,
                EmployeeId = domain.EmployeeId
            };
        }
    }
}
