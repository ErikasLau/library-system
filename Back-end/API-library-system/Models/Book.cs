namespace API_library_system.Models
{
	public class Book(int id, byte[] pictureData, string pictureContentType, string name, DateTime year) : LibraryItem(id, pictureData, pictureContentType, name, year)
	{
		public override decimal Price => 2;
	}
}
