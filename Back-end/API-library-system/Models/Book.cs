namespace API_library_system.Models
{
	public class Book(string name, DateTime year, byte[] pictureData, BookType bookType) : LibraryItem(name, year, pictureData, bookType)
	{
		public override decimal Price => 2;
	}
}
