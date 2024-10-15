using API_library_system.Data;
using API_library_system.DTO;
using API_library_system.Models;
using API_library_system.Services;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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
		private readonly ReservationServices _services;

		public ReservationsController(AppDbContext context, ReservationServices services, IMapper mapper)
		{
			_context = context;
			_services = services;
			_mapper = mapper;
		}

		// GET: api/reservations
		[HttpGet]
		public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetReservations()
		{
			return await _context.Reservations
				.Include(r => r.Book)
				.Include(r => r.TotalPrice)
				.ProjectTo<ReservationDTO>(_mapper.ConfigurationProvider)
				.ToListAsync();
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
		public async Task<ActionResult<Reservation>> PostReservation(Reservation reservation)
		{
			if (reservation.BookId <= 0)
			{
				return BadRequest();
			}

			if (reservation.FromDate > reservation.ToDate)
			{
				return BadRequest();
			}

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
			[FromQuery] DateTime fromDate,
			[FromQuery] DateTime toDate,
			[FromQuery] bool isQuickPickup,
			[FromQuery] int bookId
		)
		{
			if (bookId <= 0)
			{
				return BadRequest();
			}

			LibraryItem book = await _context.LibraryItems.FindAsync(bookId);

			if (book == null)
			{
				return NotFound();
			}

			ReservationPrice reservationPrice = _services.CalculateReservationPrice(book, fromDate, toDate, isQuickPickup);

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
