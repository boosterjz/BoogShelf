using System.ComponentModel.DataAnnotations;

namespace BookShelf.Models {
  public class Book {
    public ulong BookId { get; set; }
    public string? Title { get; set; }
    public ushort PublishingYear { get; set; }
    public string? Publishing { get; set;}
    [DataType("decimal(8,2)")]
    public decimal Price { get; set; }
    public string? Category { get; set; }
    public string? Description { get; set; }
    public int PageAmount { get; set; }
    public string? Genre { get; set; }

    public Author? Author { get; set; }
    public ulong? AuthorId { get; set; }
  }
}