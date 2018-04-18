using AzureCloudSync.SyncEngine.StorageFileInfo;
using System.Collections.Generic;

namespace AzureCloudSync.SyncEngine
{
    class PendingSyncFileInfoList
    {
        public IEnumerable<CustomFileInfo> FileInfoList { get; set; }
    }
}
