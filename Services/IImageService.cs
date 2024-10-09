namespace SurveillanceAPI.Services
{
    public interface IImageService
    {
        FileStream GetImage(string imageName);
        Task UploadImage(IFormFile file);
    }
}
