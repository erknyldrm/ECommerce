using Microsoft.AspNetCore.Http;

namespace ECommerce.Application.Abstractions.Storage
{
    public interface IStorage
    {
        Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFileCollection files);

        Task DeleteAsync(string fileName, string pathOrContainerName);

        List<string> GetFiles(string pathOrContainerName);

        bool HasFile(string fileName, string pathOrContainerName);   
    }
}
