namespace API_library_system.Models
{
	public abstract class LibraryItem(int id, byte[] pictureData, string pictureContentType, string name, DateTime year)
	{
		public int Id { get; set; } = id;
		public byte[] PictureData { get; set; } = pictureData;
		public string PictureContentType { get; set; } = pictureContentType;
		public string Name { get; set; } = name;
		public DateTime Year { get; set; } = year;

		public abstract decimal Price { get; }
	}
}
