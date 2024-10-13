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
            return await _context.Reservations.ToListAsync();
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

            LibraryItem book = _context.LibraryItems.Find(reservation.BookId);

            if (book == null)
            {
				return NotFound();
			}

            reservation.Book = book;

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReservation", new { id = reservation.Id }, reservation);
        }

		// POST: api/Price
		[Route("/api/reservationPrice")]
		[HttpGet]
		public async Task<ActionResult<Reservation>> GetReservationPrice(Reservation reservation)
		{
			int serviceFee = 3;
			int quickPickup = 5;

			decimal sum = 0;

			if (reservation.BookId <= 0)
			{
				return BadRequest();
			}

			LibraryItem book = await _context.LibraryItems.FindAsync(reservation.BookId);

			if (book == null)
			{
				return NotFound();
			}

			int days = (int)Math.Ceiling((reservation.ToDate - reservation.FromDate).TotalDays);

			sum = days * book.Price * (decimal)(days > 3 && days <= 10 ? 0.9 : 1) * (decimal)(days > 10 ? 0.8 : 1) + serviceFee + (reservation.IsQuickPickUp ? quickPickup : 0);

			return Ok(sum);
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
