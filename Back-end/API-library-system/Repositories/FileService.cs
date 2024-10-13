namespace API_library_system.Repositorie
{
	public interface IFileService
	{
		Task<byte[]> ConvertImageToBytesAsync(IFormFile imageFile, string[] allowedFileExtensions);
	}

	public class FileService()
	{

		public async Task<byte[]> ConvertImageToBytesAsync(IFormFile imageFile, string[] allowedFileExtensions)
		{
			if (imageFile == null)
			{
				throw new ArgumentNullException(nameof(imageFile));
			}

			if (!allowedFileExtensions.Any(ext => imageFile.FileName.EndsWith(ext, StringComparison.OrdinalIgnoreCase)))
			{
				throw new ArgumentException($"Only {string.Join(",", allowedFileExtensions)} are allowed.");
			}

			using (var memoryStream = new MemoryStream())
			{
				await imageFile.CopyToAsync(memoryStream);
				return memoryStream.ToArray();
			}
		}
	}
}
