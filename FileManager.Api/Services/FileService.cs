
using FileManager.Api.Entities;
using FileManager.Api.Persistence;

namespace FileManager.Api.Services
{
    public class FileService(IWebHostEnvironment webHostEnvironment , ApplicationDbContext context) : IFileService
    {
        private readonly string _filePath = $"{webHostEnvironment.WebRootPath}/Uploads";
        private readonly string _imagesPath = $"{webHostEnvironment.WebRootPath}/Images";
        private readonly ApplicationDbContext _context = context;

        public async Task<(byte[] fileContent, string contentType, string fileName)> DownLoadAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var file = await _context.Files.FindAsync(id,cancellationToken);
            if (file is null)
                return ([], string.Empty, string.Empty);

            var path = Path.Combine(_filePath, file.StoredFileName);

            MemoryStream memoryStream = new();
            using FileStream fileStream = new FileStream(path, FileMode.Open);
            fileStream.CopyTo(memoryStream);

            memoryStream.Position = 0;

            return (memoryStream.ToArray(), file.ContentType, file.FileName);
        }

        public async Task<(FileStream? stream, string contentType, string fileName)> StreamAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var file = await _context.Files.FindAsync(id, cancellationToken);
            if (file is null)
                return (null, string.Empty, string.Empty);

            var path = Path.Combine(_filePath, file.StoredFileName);

            var fileStream = File.OpenRead(path);

            return (fileStream, file.ContentType, file.FileName);
        }

        public async Task<Guid> UploadAsync(IFormFile file, CancellationToken cancellationToken = default)
        {

            var uploadedFile = await SaveFile(file, cancellationToken);

            await _context.AddAsync(uploadedFile, cancellationToken);
            await _context.SaveChangesAsync( cancellationToken);


            return uploadedFile.Id;

        }

        public async Task UploadImageAsync(IFormFile image, CancellationToken cancellationToken = default)
        {
            var path = Path.Combine(_imagesPath, image.FileName);

            using var stream = File.Create(path);
            await image.CopyToAsync(stream, cancellationToken);
        }

        public async Task<IEnumerable<Guid>> UploadManyAsync(IFormFileCollection files, CancellationToken cancellationToken = default)
        {

            List<UploadedFile> uploadedFiles = [];

            foreach (var file in files)
            {
               var  uploadedFile = await SaveFile(file, cancellationToken);
                uploadedFiles.Add(uploadedFile);
            }

            await _context.AddRangeAsync(uploadedFiles, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return uploadedFiles.Select(x=>x.Id).ToList();

        }

        private async Task<UploadedFile> SaveFile(IFormFile file, CancellationToken cancellationToken = default)
        {
            var randomFileName = Path.GetRandomFileName();

            var uploadedFile = new UploadedFile
            {
                FileName = file.FileName,
                ContentType = file.ContentType,
                FileExtension = Path.GetExtension(file.FileName),
                StoredFileName = randomFileName
            };
            var path = Path.Combine(_filePath, randomFileName);

            using var stream = File.Create(path);
            await file.CopyToAsync(stream, cancellationToken);

            return uploadedFile;

        }
    }
}
