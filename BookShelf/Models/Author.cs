namespace BookShelf.Models {
  public class Author {
    public ulong AuthorId { get; set; }
    public string Name { get; set; }
    public ushort BirthYear { get; set; }
    public ushort? DeathYear { get; set; }
    
    public List<Book> Books { get; set; }
  }
}