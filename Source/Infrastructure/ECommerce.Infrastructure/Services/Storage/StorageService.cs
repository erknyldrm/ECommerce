using ECommerce.Application.Abstractions.Storage;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Infrastructure.Services.Storage
{
    internal class StorageService : IStorageService
    {
        private readonly IStorage _storage;

        public StorageService(IStorage storage)
        {
            _storage = storage;
        }

        public string StorageName { get => _storage.GetType().Name; }

        public async Task DeleteAsync(string fileName, string pathOrContainerName) => 
            await _storage.DeleteAsync(fileName, pathOrContainerName);

        public List<string> GetFiles(string pathOrContainerName) => _storage.GetFiles(pathOrContainerName);

        public bool HasFile(string fileName, string pathOrContainerName) => _storage.HasFile(fileName, pathOrContainerName);

        public Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFileCollection files)
         => _storage.UploadAsync(pathOrContainerName, files);   
    }
}
