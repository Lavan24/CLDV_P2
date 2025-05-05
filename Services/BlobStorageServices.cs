using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.Threading.Tasks;

namespace EventEaseApplication.Services
{
    public class BlobStorageServices
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName;
        private BlobContainerClient _containerClient;

        public BlobStorageServices(string connectionString, string containerName)
        {
            // Ensure that the connection string and container name are not null
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException(nameof(connectionString), "Connection string cannot be null or empty.");

            if (string.IsNullOrEmpty(containerName))
                throw new ArgumentNullException(nameof(containerName), "Container name cannot be null or empty.");

            _blobServiceClient = new BlobServiceClient(connectionString);
            _containerName = containerName;
        }

        // Retrieves or creates the blob container client
        public async Task<BlobContainerClient> GetContainerAsync()
        {
            if (_containerClient != null)
                return _containerClient;

            _containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            await _containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);
            return _containerClient;
        }

        // Gets a specific blob client
        public BlobClient GetBlobClient(string fileName)
        {
            if (_containerClient == null)
            {
                throw new InvalidOperationException("Container has not been initialized. Call GetContainerAsync() first.");
            }
            return _containerClient.GetBlobClient(fileName);
        }
    }
}
