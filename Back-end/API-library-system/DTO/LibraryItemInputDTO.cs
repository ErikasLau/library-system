using System.ComponentModel.DataAnnotations;

namespace API_library_system.DTO
{
    public class LibraryItemInputDTO
    {
        [MaxLength(20, ErrorMessage = "Book name must be less than 20 character")]
        public string Name { get; set; }
        public DateTime PublishDate { get; set; }
        public IFormFile File { get; set; }
    }
}
