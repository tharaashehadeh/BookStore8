namespace BookStore.Models
{
	public class BookCategory
	{
		public int BookId { get; set; }
		public Book? book { get; set; }
		public int CatrgoryId { get; set; }
		public Category? category { get; set; }
	}
}
