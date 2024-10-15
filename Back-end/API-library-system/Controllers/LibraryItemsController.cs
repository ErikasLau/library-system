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
	[Route("api/library")]
	[ApiController]
	public class LibraryItemsController : ControllerBase
	{
		private readonly AppDbContext _context;
		private readonly IMapper _mapper;
		private readonly FileService _fileService;

		public LibraryItemsController(AppDbContext context, IMapper mapper, FileService fileService)
		{
			_context = context;
			_mapper = mapper;
			_fileService = fileService;
		}

		// GET: api/library
		[HttpGet]
		public async Task<ActionResult<IEnumerable<LibraryItemDTO>>> GetLibraryItems([FromQuery] string? search)
		{
			if (search != null)
			{
				return await _context.LibraryItems.Where(r => r.Name.Contains(search) || r.Year.ToString().Contains(search) || r.BookType.ToString().Contains(search)).ProjectTo<LibraryItemDTO>(_mapper.ConfigurationProvider).ToListAsync();
			}

			return await _context.LibraryItems.ProjectTo<LibraryItemDTO>(_mapper.ConfigurationProvider).ToListAsync();
		}

		// GET: api/library/5
		[HttpGet("{id}")]
		public async Task<ActionResult<LibraryItemDTO>> GetLibraryItem(int id)
		{
			var libraryItem = await _context.LibraryItems.FindAsync(id);

			if (libraryItem == null)
			{
				return NotFound();
			}

			return _mapper.Map<LibraryItemDTO>(libraryItem);
		}

		[Route("book")]
		// POST: api/library/audiobook
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		public async Task<ActionResult<LibraryItemDTO>> PostLibraryItemBook([FromForm] string Name, [FromForm] DateTime PublishDate, IFormFile File)
		{
			if (!_fileService.AllowedDataType(File))
			{
				return BadRequest();
			}

			byte[] imageBytes = await _fileService.ConvertImageToBytesAsync(File);

			Book book = new(Name, PublishDate, imageBytes, BookType.Book);

			_context.LibraryItems.Add(book);
			await _context.SaveChangesAsync();

			var mappedBook = _mapper.Map<Book, LibraryItemDTO>(book);

			return CreatedAtAction("GetLibraryItems", new { id = mappedBook.Id }, mappedBook);
		}

		[Route("audiobook")]
		// POST: api/library/audiobook
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		public async Task<ActionResult<LibraryItemDTO>> PostLibraryItemAudiobook([FromForm] string Name, [FromForm] DateTime PublishDate, IFormFile File)
		{
			var imageUtils = new FileService();
			var imageBytes = await imageUtils.ConvertImageToBytesAsync(File);

			Audiobook audiobook = new(Name, PublishDate, imageBytes, BookType.Audiobook);

			_context.LibraryItems.Add(audiobook);
			await _context.SaveChangesAsync();

			var mappedAudiobook = _mapper.Map<Audiobook, LibraryItemDTO>(audiobook);

			return CreatedAtAction("GetLibraryItems", new { id = mappedAudiobook.Id }, mappedAudiobook);
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
