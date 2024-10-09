namespace SurveillanceAPI.Services
{
    public interface IImageService
    {
        FileStream GetImage(string imageName);
        FileStream GetNewestImage();
        Task UploadImage(IFormFile file);
    }
}
