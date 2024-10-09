namespace SurveillanceAPI.Services
{
    public class ImageService : IImageService
    {
        private readonly string _uploadFolderPath;

        public ImageService()
        {
            // Set your upload folder path (ensure this folder exists)
            _uploadFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Images/Uploads");
            Directory.CreateDirectory(_uploadFolderPath); // Ensure the directory exists
        }

        public FileStream GetImage(string imageName)
        {
            var filePath = Path.Combine(_uploadFolderPath, imageName);

            //if (!System.IO.File.Exists(filePath))
            //{
            //    return NotFound("Image not found.");
            //}

            var image = File.OpenRead(filePath);
            return image;
        }

        public async Task UploadImage(IFormFile file)
        {
            // Create a unique filename to avoid overwriting files
            var fileName = Path.GetFileName(file.FileName);
            var filePath = Path.Combine(_uploadFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }
    }
}
