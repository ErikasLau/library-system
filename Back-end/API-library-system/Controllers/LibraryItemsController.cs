using API_library_system.Data;
using API_library_system.Dto;
using API_library_system.Models;
using API_library_system.Repositorie;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_library_system.Controllers
{
	[Route("api/library")]
	[ApiController]
	public class LibraryItemsController : ControllerBase
	{
		private readonly AppDbContext _context;
		private readonly IMapper _mapper;

		public LibraryItemsController(AppDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		// GET: api/library
		[HttpGet]
		public async Task<ActionResult<List<LibraryItemDto>>> GetLibraryItems([FromQuery] string? search)
		{
			if (search != null)
			{
				return await _context.LibraryItems.Where(r => r.Name.Contains(search) || r.Year.ToString().Contains(search) || r.BookType.ToString().Contains(search)).ProjectTo<LibraryItemDto>(_mapper.ConfigurationProvider).ToListAsync();
			}

			return await _context.LibraryItems.ProjectTo<LibraryItemDto>(_mapper.ConfigurationProvider).ToListAsync();
		}

		// GET: api/library/5
		[HttpGet("{id}")]
		public async Task<ActionResult<LibraryItemDto>> GetLibraryItem(int id)
		{
			var libraryItem = await _context.LibraryItems.FindAsync(id);

			if (libraryItem == null)
			{
				return NotFound();
			}

			return _mapper.Map<LibraryItemDto>(libraryItem);
		}

		[Route("book")]
		// POST: api/library/audiobook
		[HttpPost]
		public async Task<ActionResult<LibraryItemDto>> PostLibraryItemBook([FromForm] string Name, [FromForm] DateTime Year, IFormFile File)
		{
			var imageUtils = new FileService();
			var imageBytes = await imageUtils.ConvertImageToBytesAsync(File, [".jpg", ".jpeg", ".png"]);

			Book book = new(Name, Year, imageBytes, BookType.Book);

			_context.LibraryItems.Add(book);
			await _context.SaveChangesAsync();

			var map = _mapper.Map<Book, LibraryItemDto>(book);

			return map;
		}

		[Route("audiobook")]
		// POST: api/library/audiobook
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		public async Task<ActionResult<LibraryItemDto>> PostLibraryItemAudiobook([FromForm] string Name, [FromForm] DateTime Year, IFormFile File)
		{
			var imageUtils = new FileService();
			var imageBytes = await imageUtils.ConvertImageToBytesAsync(File, [".jpg", ".jpeg"]);

			Audiobook audiobook = new(Name, Year, imageBytes, BookType.Audiobook);

			_context.LibraryItems.Add(audiobook);
			await _context.SaveChangesAsync();

			var map = _mapper.Map<Audiobook, LibraryItemDto>(audiobook);

			return map;
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
