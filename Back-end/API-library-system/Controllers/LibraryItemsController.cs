using API_library_system.Data;
using API_library_system.Models;
using API_library_system.Repositorie;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_library_system.Controllers
{
	[Route("api/library")]
	[ApiController]
	public class LibraryItemsController : ControllerBase
	{
		private readonly AppDbContext _context;

		public LibraryItemsController(AppDbContext context)
		{
			_context = context;
		}

		// GET: api/library
		[HttpGet]
		public async Task<ActionResult<IEnumerable<LibraryItem>>> GetLibraryItems([FromQuery] string? search)
		{
			if (search != null)
			{
				return await _context.LibraryItems.Where(r => r.Name.Contains(search) || r.Year.ToString().Contains(search)).ToListAsync();
			}

			return await _context.LibraryItems.ToListAsync();
		}

		// GET: api/library/5
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

		[Route("book")]
		// POST: api/library/audiobook
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
		// POST: api/library/audiobook
		[HttpPost]
		public async Task<ActionResult<Audiobook>> PostLibraryItemAudiobook([FromForm] string Name, [FromForm] DateTime Year, IFormFile File)
		{
			var imageUtils = new FileService();
			var imageBytes = await imageUtils.ConvertImageToBytesAsync(File, [".jpg", ".jpeg"]);

			Audiobook audiobook = new(Name, Year, imageBytes);

			_context.LibraryItems.Add(audiobook);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetLibraryItem", new { id = audiobook.Id }, audiobook);
		}

		// DELETE: api/library/5
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
	}
}
