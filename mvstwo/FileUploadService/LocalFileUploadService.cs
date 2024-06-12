namespace mvstwo.FileUploadService
{
    public class LocalFileUploadService : IFileUploadService
    {
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment environment;
        public LocalFileUploadService(Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            this.environment = env;
        }
        public async Task<string> UploadFileAsync(IFormFile file)
        {
            var filePath = Path.Combine(environment.ContentRootPath, @"wwwroot\images\data", file.FileName);

            using var fileStream = new FileStream(filePath, FileMode.Create);
            file.CopyToAsync(fileStream);

            return file.FileName;
        }
    }
}
