using System;

namespace AzureCloudSync.StorageFileInfo
{
    public class StorageFileInfo
    {
        public string OriginalPath { get; set; }
        public string FullPath { get; set; }
        public string DirectoryFullPath { get; set; }
        public string Extension { get; set; }
        public bool IsReadOnly { get; set; }
        public DateTime LastWriteTime { get; set; }
        public DateTime LastWriteTimeUtc { get; set; }
        public long Length { get; set; }
    }
}
