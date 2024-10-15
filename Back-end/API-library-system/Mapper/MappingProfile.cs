using API_library_system.DTO;
using API_library_system.Models;
using AutoMapper;

namespace API_library_system.Mapper
{
	public class MappingProfile : Profile
	{

		public MappingProfile()
		{
			CreateMap<LibraryItem, LibraryItemDTO>()
				.ForMember(dest => dest.BookType, opt => opt.MapFrom(src => src.BookType));
			CreateMap<Book, LibraryItemDTO>()
				.ForMember(dest => dest.BookType, opt => opt.MapFrom(src => src.BookType));
			CreateMap<Audiobook, LibraryItemDTO>()
				.ForMember(dest => dest.BookType, opt => opt.MapFrom(src => src.BookType));

			CreateMap<ReservationPrice, ReservationPriceDTO>();
			CreateMap<Reservation, ReservationDTO>()
				.ForMember(dest => dest.Book, opt => opt.MapFrom((reservation, reservationDTO, i, context) =>
				{
					if (reservation.Book != null)
					{
						return context.Mapper.Map<LibraryItem, LibraryItemDTO>(reservation.Book);
					}
					return null;
				})
				)
				.ForMember(dest => dest.TotalPrice, opt => opt.MapFrom((reservation, reservationDTO, i, context) =>
				{
					if (reservation.TotalPrice != null)
					{
						return context.Mapper.Map<ReservationPrice, ReservationPriceDTO>(reservation.TotalPrice);
					}
					return null;
				})
				);
		}
	}
}
