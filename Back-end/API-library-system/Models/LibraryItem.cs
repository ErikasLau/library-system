namespace API_library_system.Models
{
	public abstract class LibraryItem
	{
		public int Id { get; set; }
		public byte[] PictureData { get; set; }
		public string Name { get; set; }
		public DateTime Year { get; set; }
		public string BookType { get; }

		public LibraryItem(string name, DateTime year, byte[] pictureData)
		{
			Name = name;
			Year = year;
			PictureData = pictureData;
			BookType = GetType().Name;
		}

		public abstract decimal Price { get; }
	}
}
