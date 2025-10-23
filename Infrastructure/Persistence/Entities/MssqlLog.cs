using Domain.Entities;
using Infrastructure.Persistence.Interfaces;
using System;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Persistence.Entities
{
  public class MssqlLog : MssqlEntity<Log>
  {
    protected override string TableName => "log";

    public DateTime Timestamp { get; set; }
    public string? EventType { get; set; }
    public string? Description { get; set; }
    public Guid? BookCopyId { get; set; }
    public Guid? CustomerId { get; set; }
    public Guid? EmployeeId { get; set; }

    public override string UpdateQuery => $@"update {TableName}
set timestamp='{Timestamp:yyyy-MM-dd HH:mm:ss}',
    event_type={(EventType == null ? "NULL" : $"'{EventType}'")},
    description={(Description == null ? "NULL" : $"'{Description}'")},
    book_copy_id={(BookCopyId == null ? "NULL" : $"'{BookCopyId}'")},
    customer_id={(CustomerId == null ? "NULL" : $"'{CustomerId}'")},
    employee_id={(EmployeeId == null ? "NULL" : $"'{EmployeeId}'")}
where id='{Id}'";

    public override string InsertQuery => $@"insert into {TableName}
(id, timestamp, event_type, description, book_copy_id, customer_id, employee_id)
values ('{Id}', '{Timestamp:yyyy-MM-dd HH:mm:ss}', {(EventType == null ? "NULL" : $"'{EventType}'")}, {(Description == null ? "NULL" : $"'{Description}'")}, {(BookCopyId == null ? "NULL" : $"'{BookCopyId}'")}, {(CustomerId == null ? "NULL" : $"'{CustomerId}'")}, {(EmployeeId == null ? "NULL" : $"'{EmployeeId}'")})";

    public override void AssignFromReader(SqlDataReader reader)
    {
      Id = Guid.Parse(reader["id"].ToString());
      Timestamp = DateTime.Parse(reader["timestamp"].ToString());
      EventType = reader["event_type"].ToString();
      Description = reader["description"].ToString();
      BookCopyId = reader["book_copy_id"] == DBNull.Value ? (Guid?)null : Guid.Parse(reader["book_copy_id"].ToString());
      CustomerId = reader["customer_id"] == DBNull.Value ? (Guid?)null : Guid.Parse(reader["customer_id"].ToString());
      EmployeeId = reader["employee_id"] == DBNull.Value ? (Guid?)null : Guid.Parse(reader["employee_id"].ToString());
    }
  }
}
