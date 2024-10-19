using Microsoft.AspNetCore.Components.Forms;

namespace BouillonChanvre.Services
{
    public interface IBlobStorageService
    {
        Task<string> UploadFileAsync(string containerName, IBrowserFile file);
        Task DeleteFileAsync(string containerName, string blobUri);
    }
}