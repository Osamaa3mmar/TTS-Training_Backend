using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
namespace Backend.PL
{
    public class FilesUtiles
    {
        private readonly Cloudinary cloudinary;
        public FilesUtiles(Cloudinary cloudinary)
        {
            
            this.cloudinary = cloudinary;
        }
        

        public async Task<string > UploadImage(IFormFile image) {

            if (image == null || image.Length == 0)
                return null;

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(image.FileName, image.OpenReadStream()),
                Transformation = new Transformation().Crop("scale").Width(500).Height(500)
            };
            var result = await cloudinary.UploadAsync(uploadParams);
            return result.SecureUrl.ToString();
        }
    }
}
