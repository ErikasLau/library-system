using API_library_system.Models;

namespace API_library_system.Dto
{
	public class LibraryItemDto
	{
		public int Id { get; set; }
		public byte[] PictureData { get; set; }
		public string Name { get; set; }
		public DateTime Year { get; set; }
		public string BookType { get; set; }
		public decimal Price { get; set; }
	}
}
