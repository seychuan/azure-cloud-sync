using AzureCloudSync.AzureCloudUtility;
using System.Threading.Tasks;

namespace AzureCloudSync.SyncEngine
{
    public class AzureBlobStorageSyncEngine
    {
        private readonly string _containerName;
        private readonly string _subscriberId;

        public AzureBlobStorageSyncEngine(string containerName, string subscriberId)
        {
            _containerName = containerName;
            _subscriberId = subscriberId;
        }

        public async Task RunAsync()
        {
            var cloudBlobClient = new CloudBlobClient(_containerName, _subscriberId);

            await cloudBlobClient.DownloadCloudBlockBlobAsync("", "").ConfigureAwait(false);





        }
    }
}
