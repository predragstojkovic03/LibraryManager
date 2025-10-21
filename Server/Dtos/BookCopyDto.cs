namespace Server.Dtos
{
  public class BookCopyDto
  {
    public Guid Id { get; set; }
    public Guid LibraryId { get; set; }
    public Guid BookId { get; set; }
    public Guid BorrowerId { get; set; }
    public DateOnly PrintDate { get; set; }
  }
}
