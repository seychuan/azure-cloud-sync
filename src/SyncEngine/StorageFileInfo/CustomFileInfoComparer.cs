using System;
using System.Collections.Generic;

namespace AzureCloudSync.SyncEngine.StorageFileInfo
{
    public class CustomFileInfoComparer : IEqualityComparer<CustomFileInfo>
    {
        public bool Equals(CustomFileInfo x, CustomFileInfo y)
        {
            if (string.Equals(x.FullPath, y.FullPath, StringComparison.OrdinalIgnoreCase)) return true;
            return false;
        }

        public int GetHashCode(CustomFileInfo obj)
        {
            return obj.FullPath.GetHashCode();
        }
    }
}
