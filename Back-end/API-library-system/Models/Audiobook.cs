namespace API_library_system.Models
{
	public class Audiobook(string name, DateTime year, byte[] pictureData) : LibraryItem(name, year, pictureData)
	{
		public override decimal Price => 3;
	}
}
