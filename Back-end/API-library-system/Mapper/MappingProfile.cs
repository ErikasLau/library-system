using API_library_system.Dto;
using API_library_system.Models;
using AutoMapper;

namespace API_library_system.Mapper
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<LibraryItem, LibraryItemDto>()
				.ForMember(dest => dest.BookType, opt => opt.MapFrom(src => src.BookType));
			CreateMap<Book, LibraryItemDto>()
				.ForMember(dest => dest.BookType, opt => opt.MapFrom(src => src.BookType));
			CreateMap<Audiobook, LibraryItemDto>()
				.ForMember(dest => dest.BookType, opt => opt.MapFrom(src => src.BookType));
		}
	}
}
