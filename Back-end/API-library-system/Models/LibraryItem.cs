namespace API_library_system.Models
{
	public abstract class LibraryItem
	{
		public int Id { get; set; }
		public byte[] PictureData { get; set; }
		public string Name { get; set; }
		public DateTime Year { get; set; }
		public BookType BookType { get; set; }

		public LibraryItem(string name, DateTime year, byte[] pictureData, BookType bookType)
		{
			Name = name;
			Year = year;
			PictureData = pictureData;
			BookType = bookType;
		}

		public abstract decimal Price { get; }
	}

	public enum BookType
	{
		Book,
		Audiobook
	}
}
