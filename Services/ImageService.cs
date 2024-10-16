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

        public FileStream GetNewestImage()
        {
            // Get all image files from the directory
            var files = Directory.GetFiles(_uploadFolderPath)
                                 .Where(file => file.EndsWith(".jpg") || file.EndsWith(".jpeg") || file.EndsWith(".png"))
                                 .OrderByDescending(file => new FileInfo(file).CreationTime)
                                 .ToList();

            //if (!files.Any())
            //{
            //    return NotFound("No images found.");
            //}

            // Get the most recent image
            var newestFile = files.First();
            var image = File.OpenRead(newestFile);

            // Get the file extension and set the correct MIME type
            //string mimeType = "image/" + Path.GetExtension(newestFile).TrimStart('.').ToLower();

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

        public string Test(string filename)
        {
            var filePath = Path.Combine(_uploadFolderPath, filename);
            if (File.Exists(filePath))
            {
                return filePath + " It exists";
            }
            else
            {
                return filePath + " It does not exist";
            }
        }
    }
}
