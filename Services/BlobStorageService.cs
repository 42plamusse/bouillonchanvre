using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BouillonChanvre.Services;
using Microsoft.AspNetCore.Components.Forms;

public class BlobStorageService : IBlobStorageService
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly string _containerName;

    public BlobStorageService(string connectionString)
    {
        _blobServiceClient = new BlobServiceClient(connectionString);
    }

    public async Task<string> UploadFileAsync(string containerName, IBrowserFile file)
    {
        try
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

            // Try to create the container, but handle the case where it already exists
            try
            {
                await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);
            }
            catch (Azure.RequestFailedException ex) when (ex.Status == 409)
            {
                Console.WriteLine("Container already exists.");
            }

            // Generate a unique ID for the blob name (GUID) and retain the original file extension
            var uniqueBlobName = $"{Guid.NewGuid()}{Path.GetExtension(file.Name)}";

            var blobClient = containerClient.GetBlobClient(uniqueBlobName);

            // Increase the maximum file size to 10MB (or any appropriate size for your use case)
            var maxAllowedSize = 10 * 1024 * 1024; // 10 MB

            var stream = file.OpenReadStream(maxAllowedSize);

            // Prepare metadata with the original file name
            var metadata = new Dictionary<string, string>
        {
            { "OriginalFileName", file.Name }
        };

            // Upload the file to Azure Blob Storage
            await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = file.ContentType });

            // Set metadata for the blob
            await blobClient.SetMetadataAsync(metadata);

            // Return the URI of the uploaded file
            var fileUri = blobClient.Uri.ToString();
            return fileUri;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error encountered: {ex.Message}");
            throw;
        }
    }

    public async Task DeleteFileAsync(string containerName, string blobUri)
    {
        try
        {
            // Get the container client
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

            // Extract the blob name from the URI
            var blobName = Path.GetFileName(new Uri(blobUri).LocalPath);

            // Get the blob client
            var blobClient = containerClient.GetBlobClient(blobName);

            // Check if the blob exists before attempting to delete
            if (await blobClient.ExistsAsync())
            {
                // Delete the blob
                await blobClient.DeleteIfExistsAsync();
                Console.WriteLine($"File {blobName} deleted successfully.");
            }
            else
            {
                Console.WriteLine($"File {blobName} not found in container {containerName}.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error encountered while deleting file: {ex.Message}");
            throw;
        }
    }


}
