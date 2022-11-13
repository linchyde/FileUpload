using Microsoft.AspNetCore.Components.Forms;

namespace FileUpload.Services
{
    public interface IFileUpload
    {
        Task UploadFile(IBrowserFile file);
    }
    public class FileUploads : IFileUpload
    {
        private IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<FileUploads> _logger;

        public FileUploads(IWebHostEnvironment webHostEnvironment, ILogger<FileUploads> logger)
        {
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        public async Task UploadFile(IBrowserFile file)
        {
            if(file is not null)
            {
                try
                {
                    var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", file.Name);

                    using (var stream = file.OpenReadStream())
                    {
                        var fileStream = File.Create(uploadPath);
                        await stream.CopyToAsync(fileStream);
                        fileStream.Close();
                    }


                }catch(Exception ex) 
                {
                    _logger.LogError(ex.ToString());    
                }
            }
        }
    }
}
