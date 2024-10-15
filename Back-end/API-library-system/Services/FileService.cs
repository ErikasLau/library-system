namespace API_library_system.Services
{
	public interface IFileService
	{
		Task<byte[]> ConvertImageToBytesAsync(IFormFile imageFile);
		Boolean CheckAllowedDataType(IFormFile file);
	}

	public class FileService() : IFileService
	{
		public readonly string[] AllowedFileExtensions = [".jpg", ".jpeg", ".png"];

		public async Task<byte[]> ConvertImageToBytesAsync(IFormFile imageFile)
		{
			ArgumentNullException.ThrowIfNull(imageFile);

			if (!CheckAllowedDataType(imageFile))
			{
				throw new ArgumentException($"Only {string.Join(",", AllowedFileExtensions)} are allowed.");
			}

			using var memoryStream = new MemoryStream();
			await imageFile.CopyToAsync(memoryStream);
			return memoryStream.ToArray();
		}

		public Boolean CheckAllowedDataType(IFormFile imageFile)
		{
			if (imageFile == null || !AllowedFileExtensions.Any(ext => imageFile.FileName.EndsWith(ext, StringComparison.OrdinalIgnoreCase)))
			{
				return false;
			}

			return true;
		}
	}
}
