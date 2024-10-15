namespace API_library_system.DTO
{
	public class LibraryItemDTO
	{
		public int Id { get; set; }
		public byte[] PictureData { get; set; }
		public string Name { get; set; }
		public DateTime Year { get; set; }
		public string BookType { get; set; }
		public decimal Price { get; set; }
	}
}
