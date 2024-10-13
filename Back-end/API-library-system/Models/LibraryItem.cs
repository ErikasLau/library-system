using AutoMapper;

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

	public class LibraryItemDto
	{
		public int Id { get; }
		public string Name { get; set; }
		public DateTime Year { get; set; }
		public IFormFile File { get; set; }
	}

	public class LibraryItemProfile : Profile
	{
		public LibraryItemProfile()
		{
			CreateMap<LibraryItem, LibraryItemDto>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
				.ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year));
				//.ForMember(dest => dest.)

		}
	}
}
