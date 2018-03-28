using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using System.Threading.Tasks;

namespace AzureCloudSync.AzureCloudUtility
{
    public class CloudBlobClient
    {
        private CloudBlobContainer _blobContainer;
        private string _subscriberId;

        public CloudBlobClient(string containerName, string subscriberId)
        {
            var storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            var blobClient = storageAccount.CreateCloudBlobClient();

            _blobContainer = blobClient.GetContainerReference(containerName);
            _subscriberId = subscriberId;
        }

        public async Task UploadCloudBlockBlobAsync(string blobName, string localPath)
        {
            var blockBlob = _blobContainer.GetBlockBlobReference(_subscriberId + "/" + blobName);
            await blockBlob.UploadFromFileAsync(localPath).ConfigureAwait(false);
        }

        public async Task DeleteCloudBlockBlobAsync(string blobName)
        {
            var blockBlob = _blobContainer.GetBlockBlobReference(_subscriberId + "/" + blobName);
            await blockBlob.DeleteAsync().ConfigureAwait(false);
        }

        public async Task DownloadCloudBlockBlobAsync(string blobName, string localPath)
        {
            var blockBlob = _blobContainer.GetBlockBlobReference(_subscriberId + "/" + blobName);

            //if (Directory.Exists(localFile.DirectoryFullPath) == false)
            //{
            //    Directory.CreateDirectory(localFile.DirectoryFullPath);
            //}

            await blockBlob.DownloadToFileAsync(localPath, FileMode.Create).ConfigureAwait(false);

            //if (lastWriteTime.HasValue) File.SetLastWriteTime(localPath, lastWriteTime.Value);
            //if (lastWriteTimeUtc.HasValue) File.SetLastWriteTimeUtc(localPath, lastWriteTimeUtc.Value);

            //if (localFile.IsReadOnly) File.SetAttributes(localFile.FullPath, File.GetAttributes(localFile.FullPath) | FileAttributes.ReadOnly);
        }
    }
}
