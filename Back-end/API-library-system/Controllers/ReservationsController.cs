using API_library_system.Data;
using API_library_system.DTO;
using API_library_system.Models;
using API_library_system.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_library_system.Controllers
{
	[Route("api/reservations")]
	[ApiController]
	public class ReservationsController : ControllerBase
	{
		private readonly AppDbContext _context;
		private readonly IMapper _mapper;
		private readonly IReservationServices _services;

		public ReservationsController(AppDbContext context, IReservationServices services, IMapper mapper)
		{
			_context = context;
			_services = services;
			_mapper = mapper;
		}

		// GET: api/reservations
		[HttpGet]
		public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetReservations()
		{
			var reservations = await _context.Reservations
				.Include(r => r.Book)
				.Include(r => r.TotalPrice)
				.ToListAsync();

			return _mapper.Map<IEnumerable<Reservation>, List<ReservationDTO>>(reservations);
		}

		// GET: api/reservations/5
		[HttpGet("{id}")]
		public async Task<ActionResult<ReservationDTO>> GetReservation(int id)
		{
			var reservation = await _context.Reservations.FindAsync(id);

			if (reservation == null)
			{
				return NotFound();
			}

			return _mapper.Map<ReservationDTO>(reservation);
		}

		// POST: api/reservations
		[HttpPost]
		public async Task<ActionResult<Reservation>> PostReservation(ReservationInputDTO reservationInputDTO)
		{
			if (reservationInputDTO.BookId <= 0)
			{
				return BadRequest();
			}

			if (reservationInputDTO.FromDate < DateTime.Now.Date)
			{
				return BadRequest();
			}

			if (reservationInputDTO.FromDate > reservationInputDTO.ToDate)
			{
				return BadRequest();
			}

			var reservation = new Reservation(reservationInputDTO.FromDate, reservationInputDTO.ToDate, reservationInputDTO.IsQuickPickUp, reservationInputDTO.BookId);

			LibraryItem book = await _context.LibraryItems.FindAsync(reservation.BookId);

			if (book == null)
			{
				return NotFound();
			}

			ReservationPrice reservationPrice = _services.CalculateReservationPrice(book, reservation.FromDate, reservation.ToDate, reservation.IsQuickPickUp);

			reservation.Book = book;
			reservation.TotalPrice = reservationPrice;

			_context.Reservations.Add(reservation);
			await _context.SaveChangesAsync();

			var mappedReservation = _mapper.Map<ReservationDTO>(reservation);

			return CreatedAtAction("GetReservation", new { id = mappedReservation.Id }, mappedReservation);
		}

		// POST: api/reservationPrice
		[Route("/api/reservationPrice")]
		[HttpGet]
		public async Task<ActionResult<ReservationPrice>> GetReservationPrice(
			[FromQuery] ReservationPriceInputDTO reservationPriceInputDTO
		)
		{
			if (reservationPriceInputDTO.BookId <= 0)
			{
				return BadRequest();
			}

			if (reservationPriceInputDTO.FromDate < DateTime.Now.Date)
			{
				return BadRequest();
			}

			if (reservationPriceInputDTO.FromDate > reservationPriceInputDTO.ToDate)
			{
				return BadRequest();
			}


			LibraryItem book = await _context.LibraryItems.FindAsync(reservationPriceInputDTO.BookId);

			if (book == null)
			{
				return NotFound();
			}

			ReservationPrice reservationPrice = _services.CalculateReservationPrice(book, reservationPriceInputDTO.FromDate, reservationPriceInputDTO.ToDate, reservationPriceInputDTO.IsQuickPickup);

			var mapperReservationPrice = _mapper.Map<ReservationPriceDTO>(reservationPrice);

			return Ok(mapperReservationPrice);
		}

		// DELETE: api/reservations/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteReservation(int id)
		{
			var reservation = await _context.Reservations.FindAsync(id);
			if (reservation == null)
			{
				return NotFound();
			}

			_context.Reservations.Remove(reservation);
			await _context.SaveChangesAsync();

			return NoContent();
		}
	}
}
