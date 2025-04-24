namespace FileManager.Api.Services
{
    public interface IFileService
    {
        Task<(byte[] fileContent, string contentType, string fileName)> DownLoadAsync(Guid id, CancellationToken cancellationToken = default);
        Task<(FileStream? stream, string contentType, string fileName)> StreamAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Guid> UploadAsync(IFormFile file, CancellationToken cancellationToken = default);
        Task<IEnumerable<Guid>> UploadManyAsync(IFormFileCollection files, CancellationToken cancellationToken = default);
        Task UploadImageAsync(IFormFile image, CancellationToken cancellationToken = default);

     
    }
}
