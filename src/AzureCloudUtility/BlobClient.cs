using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AzureCloudSync.AzureCloudUtility
{
    public class BlobClient
    {
        private readonly CloudBlobContainer _blobContainer;

        public string SubscriberId { get; private set; }

        public BlobClient(string accountName, string accountKey, string containerName, string subscriberId)
        {
            var storageConnectionString = $"DefaultEndpointsProtocol=https;AccountName={accountName};AccountKey={accountKey};EndpointSuffix=core.windows.net";
            var storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();

            _blobContainer = blobClient.GetContainerReference(containerName);

            SubscriberId = subscriberId;
        }

        public async Task UploadCloudBlockBlobAsync(string blobName, string localPath)
        {
            var blockBlob = _blobContainer.GetBlockBlobReference(SubscriberId + "/" + blobName);
            await blockBlob.UploadFromFileAsync(localPath).ConfigureAwait(false);
        }

        public async Task DeleteCloudBlockBlobAsync(string blobName)
        {
            var blockBlob = _blobContainer.GetBlockBlobReference(SubscriberId + "/" + blobName);
            await blockBlob.DeleteAsync().ConfigureAwait(false);
        }

        public async Task DownloadCloudBlockBlobAsync(string blobName, string localPath)
        {
            var blockBlob = _blobContainer.GetBlockBlobReference(SubscriberId + "/" + blobName);

            //if (Directory.Exists(localFile.DirectoryFullPath) == false)
            //{
            //    Directory.CreateDirectory(localFile.DirectoryFullPath);
            //}

            await blockBlob.DownloadToFileAsync(localPath, FileMode.Create).ConfigureAwait(false);

            //if (lastWriteTime.HasValue) File.SetLastWriteTime(localPath, lastWriteTime.Value);
            //if (lastWriteTimeUtc.HasValue) File.SetLastWriteTimeUtc(localPath, lastWriteTimeUtc.Value);

            //if (localFile.IsReadOnly) File.SetAttributes(localFile.FullPath, File.GetAttributes(localFile.FullPath) | FileAttributes.ReadOnly);
        }

        public async Task<IEnumerable<IListBlobItem>> ListBlobs()
        {
            var subscriberDirectory = _blobContainer.GetDirectoryReference(SubscriberId);
            var blobItemList = new List<IListBlobItem>();

            BlobContinuationToken token = null;
            do
            {
                BlobResultSegment resultSegment = await subscriberDirectory.ListBlobsSegmentedAsync(token).ConfigureAwait(false);
                token = resultSegment.ContinuationToken;
                foreach (IListBlobItem blob in resultSegment.Results)
                {
                    blobItemList.AddRange(resultSegment.Results);
                }
            } while (token != null);

            return blobItemList;
        }
    }
}
