using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_library_system.Data;
using API_library_system.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using API_library_system.DTO;

namespace API_library_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReservationsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Reservations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations()
        {
            return await _context.Reservations
                .Include(r => r.Book)
                .ToListAsync();
        }

        // GET: api/Reservations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reservation>> GetReservation(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);

            if (reservation == null)
            {
                return NotFound();
            }

            return reservation;
        }

		// PUT: api/Reservations/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
        public async Task<IActionResult> PutReservation(int id, Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return BadRequest();
            }

            _context.Entry(reservation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Reservations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Reservation>> PostReservation(Reservation reservation)
        {
            if(reservation.BookId <= 0){
				return BadRequest();
			}

            LibraryItem book = await _context.LibraryItems.FindAsync(reservation.BookId);

            if (book == null)
            {
				return NotFound();
			}
            
            reservation.Book = book;
            reservation.CreatedAt = DateTime.Now;

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReservation", new { id = reservation.Id }, reservation);
        }

		// POST: api/Price
		[Route("/api/reservationPrice")]
		[HttpGet]
		public async Task<ActionResult<ReservationPriceDto>> GetReservationPrice(
			[FromQuery] long createdAt,
			[FromQuery] DateTime fromDate,
			[FromQuery] DateTime toDate,
			[FromQuery] bool isQuickPickup,
			[FromQuery] int bookId
		)
		{
			int serviceFee = 3;
			int quickPickup = 5;

			decimal sum = 0;
			decimal discount = 0;

			if (bookId <= 0)
			{
				return BadRequest();
			}

			LibraryItem book = await _context.LibraryItems.FindAsync(bookId);

			if (book == null)
			{
				return NotFound();
			}

			int days = (int)Math.Ceiling((toDate - fromDate).TotalDays);

			decimal discountRate = (decimal)(days > 3 && days <= 10 ? 0.1 : days > 10 ? 0.2 : 0);
			discount = days * book.Price * discountRate;
			sum = days * book.Price * (1 - discountRate) + serviceFee + (isQuickPickup ? quickPickup : 0);

			var response = new ReservationPriceDto
			{
				TotalSum = sum,
				DiscountSum = discount,
				TotalDays = days,
				ServiceFee = serviceFee,
				QuickPickupFee = isQuickPickup ? quickPickup : 0
			};

			return Ok(response);
		}

		// DELETE: api/Reservations/5
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

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }
    }
}
