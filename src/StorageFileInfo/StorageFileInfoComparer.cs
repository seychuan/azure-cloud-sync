using System;
using System.Collections.Generic;

namespace AzureCloudSync.StorageFileInfo
{
    public class StorageFileInfoComparer : IEqualityComparer<StorageFileInfo>
    {
        public bool Equals(StorageFileInfo x, StorageFileInfo y)
        {
            if (string.Equals(x.FullPath, y.FullPath, StringComparison.OrdinalIgnoreCase)) return true;
            return false;
        }

        public int GetHashCode(StorageFileInfo obj)
        {
            return obj.FullPath.GetHashCode();
        }
    }
}
