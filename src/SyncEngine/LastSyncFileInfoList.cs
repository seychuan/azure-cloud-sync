using AzureCloudSync.SyncEngine.StorageFileInfo;
using System;
using System.Collections.Generic;

namespace AzureCloudSync.SyncEngine
{
    public class LastSyncFileInfoList
    {
        public DateTime LastSyncDateTime { get; set; }
        public IEnumerable<CustomFileInfo> FileInfoList { get; set; }
    }
}
