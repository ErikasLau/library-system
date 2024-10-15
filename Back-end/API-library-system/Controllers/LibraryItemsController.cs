using API_library_system.Data;
using API_library_system.DTO;
using API_library_system.Models;
using API_library_system.Services;
using AutoMapper;
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
		private readonly IFileService _fileService;

		public LibraryItemsController(AppDbContext context, IMapper mapper, IFileService fileService)
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
				var searchedLibrary = await _context.LibraryItems.Where(r => r.Name.Contains(search) || r.Year.ToString().Contains(search) || r.BookType.ToString().Contains(search)).ToListAsync();
				return _mapper.Map<IEnumerable<LibraryItem>, List<LibraryItemDTO>>(searchedLibrary);
			}

			var library = await _context.LibraryItems.ToListAsync();
			return _mapper.Map<IEnumerable<LibraryItem>, List<LibraryItemDTO>>(library);
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
		public async Task<ActionResult<LibraryItemDTO>> PostLibraryItemBook([FromForm] LibraryItemInputDTO libraryItemInputDTO)
		{
			if (!_fileService.CheckAllowedDataType(libraryItemInputDTO.File))
			{
				return BadRequest();
			}

			byte[] imageBytes = await _fileService.ConvertImageToBytesAsync(libraryItemInputDTO.File);

			Book book = new(libraryItemInputDTO.Name, libraryItemInputDTO.PublishDate, imageBytes, BookType.Book);

			_context.LibraryItems.Add(book);
			await _context.SaveChangesAsync();

			var mappedBook = _mapper.Map<Book, LibraryItemDTO>(book);

			return CreatedAtAction("GetLibraryItems", new { id = mappedBook.Id }, mappedBook);
		}

		[Route("audiobook")]
		// POST: api/library/audiobook
		[HttpPost]
		public async Task<ActionResult<LibraryItemDTO>> PostLibraryItemAudiobook([FromForm] LibraryItemInputDTO libraryItemInputDTO)
		{
			var imageUtils = new FileService();
			var imageBytes = await imageUtils.ConvertImageToBytesAsync(libraryItemInputDTO.File);

			Audiobook audiobook = new(libraryItemInputDTO.Name, libraryItemInputDTO.PublishDate, imageBytes, BookType.Audiobook);

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
