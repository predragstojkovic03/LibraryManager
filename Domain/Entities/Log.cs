using System;

namespace Domain.Entities
{
  public class Log : IEntity
  {
    public Guid Id { get; set; }
    public DateTime Timestamp { get; set; }
    public string EventType { get; set; } // e.g. "Borrow", "Return"
    public string Description { get; set; }
    public Guid? BookCopyId { get; set; }
    public Guid? CustomerId { get; set; }
    public Guid? EmployeeId { get; set; }
  }
}
