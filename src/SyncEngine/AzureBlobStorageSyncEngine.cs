using AzureCloudSync.SyncEngine.Common;
using AzureCloudSync.SyncEngine.StorageClient;
using AzureCloudSync.SyncEngine.StorageFileInfo;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace AzureCloudSync.SyncEngine
{
    public class AzureBlobStorageSyncEngine
    {
        private readonly BlobClient _blobClient;
        private readonly string _localStoragePath;

        private LastSyncFileInfoList _lastSync;
        private PendingSyncFileInfoList _pendingSync;

        public AzureBlobStorageSyncEngine(string accountName, string accountKey, string containerName, string subscriberId, string localStoragePath)
        {
            _blobClient = new BlobClient(accountName, accountKey, containerName, subscriberId);
            _localStoragePath = localStoragePath;
        }

        public async Task RunAsync()
        {
            var lastSyncFileInfoList = LoadLastSyncFileInfoList();
            var pendingSyncFileInfoList = LoadPendingSyncFileInfoList();
            var localFileInfoList = ObtainLocalFileInfoList();
            var cloudFileInfoList = ObtainCloudFileInfoList();



            await cloudBlobClient.DownloadCloudBlockBlobAsync("", "").ConfigureAwait(false);





        }


        private string GetMetadataFile(string localStoragePath)
        {
            var metadataPath = Path.Combine(localStoragePath, _blobClient.SubscriberId);


        }

        private LastSyncFileInfoList LoadLastSyncFileInfoList()
        {
            var lastSyncPath = Path.Combine(_localStoragePath, _blobClient.SubscriberId + ".lastsync");

            if (File.Exists(lastSyncPath) == false) return null;

            using (var reader = new StreamReader(lastSyncPath))
            {
                var data = string.Empty;
                do
                {
                    data = reader.ReadLine();

                    var segments = data.Split('=');




                } (string.IsNullOrEmpty(data) == false);



            }


                var metadataPath = Path.Combine(localStoragePath, metadataName);
            var lastSyncTime = new DateTime();

            if (File.Exists(metadataPath) == false) return DateTime.MinValue;

            using (var reader = new StreamReader(metadataPath))
            {
                lastSyncTime = DateTime.FromBinary(long.Parse(reader.ReadToEnd()));
                reader.Close();
            }

            return lastSyncTime;
        }

        private PendingSyncFileInfoList LoadPendingSyncFileInfoList()
        {


        }

        private IEnumerable<CustomFileInfo> ObtainLocalFileInfoList()
        {
            var directoryInfo = new DirectoryInfo(_localStoragePath);

            if (directoryInfo.Exists)
            {
                var files = directoryInfo.GetFiles("*", SearchOption.AllDirectories).Where(f => (f.Extension != "lastsync" && f.Extension != "pending"));
                var json = JsonConvert.SerializeObject(files, Formatting.Indented, new FileInfoJsonConverter());

                return JsonConvert.DeserializeObject<IEnumerable<CustomFileInfo>>(json, new FileInfoJsonConverter());
            }
            else
            {
                return Enumerable.Empty<CustomFileInfo>();
            }
        }

        private IEnumerable<CustomFileInfo> ObtainCloudFileInfoList()
        {
            List<CustomFileInfo> fileInfoList = new List<CustomFileInfo>();

            var blobList = _blobClient.ListBlobs().Result;

            foreach (IListBlobItem blob in blobList)
            {
                var blobName = blob.Uri.Segments.Last();

                fileInfoList.Add(new CustomFileInfo().DecodeFromBlobName(blobName, _localStoragePath));
            }

            return fileInfoList;
        }
    }
}
