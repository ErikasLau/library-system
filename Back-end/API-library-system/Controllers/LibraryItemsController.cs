using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_library_system.Data;
using API_library_system.Models;
using AutoMapper;
using API_library_system.Repositorie;

namespace API_library_system.Controllers
{
    [Route("api/library")]
    [ApiController]
    public class LibraryItemsController : ControllerBase
    {
        private readonly AppDbContext _context;
        //private readonly IMapper _mapper;

        public LibraryItemsController(AppDbContext context)
        {
            _context = context;
            //_mapper = mapper;
        }

        // GET: api/LibraryItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LibraryItem>>> GetLibraryItems()
        {
			return await _context.LibraryItems.ToListAsync();
        }

        // GET: api/LibraryItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LibraryItem>> GetLibraryItem(int id)
        {
            var libraryItem = await _context.LibraryItems.FindAsync(id);

            if (libraryItem == null)
            {
                return NotFound();
            }

            return libraryItem;
        }

        // PUT: api/LibraryItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLibraryItem(int id, LibraryItem libraryItem)
        {
            if (id != libraryItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(libraryItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LibraryItemExists(id))
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

		[Route("book")]
		// POST: api/LibraryItems
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
        public async Task<ActionResult<Book>> PostLibraryItemBook([FromForm] string Name, [FromForm] DateTime Year, IFormFile File)
        {
			var imageUtils = new FileService();
			var imageBytes = await imageUtils.ConvertImageToBytesAsync(File, [".jpg", ".jpeg", ".png"]);

            Book book = new(Name, Year, imageBytes);

			_context.LibraryItems.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLibraryItem", new { id = book.Id }, book);
        }

		[Route("audiobook")]
		// POST: api/LibraryItems
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<Audiobook>> PostLibraryItemAudiobook([FromForm] string Name, [FromForm] DateTime Year, IFormFile File)
		{
			var imageUtils = new FileService();
			var imageBytes = await imageUtils.ConvertImageToBytesAsync(File, [".jpg", ".jpeg", ".png"]);

            Audiobook audiobook = new(Name, Year, imageBytes);

			_context.LibraryItems.Add(audiobook);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetLibraryItem", new { id = audiobook.Id }, audiobook);
		}

		// DELETE: api/LibraryItems/5
		[HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLibraryItem(int id)
        {
            var libraryItem = await _context.LibraryItems.FindAsync(id);
            if (libraryItem == null)
            {
                return NotFound();
            }

            _context.LibraryItems.Remove(libraryItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LibraryItemExists(int id)
        {
            return _context.LibraryItems.Any(e => e.Id == id);
        }
    }
}
