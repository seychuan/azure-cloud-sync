namespace AzureCloudSync.StorageFileInfo
{
    public static class StorageFileInfoExtension
    {
        public static StorageFileInfo Clone(this StorageFileInfo fileInfo)
        {
            return new StorageFileInfo()
            {
                OriginalPath = fileInfo.OriginalPath,
                FullPath = fileInfo.FullPath,
                DirectoryFullPath = fileInfo.DirectoryFullPath,
                Extension = fileInfo.Extension,
                IsReadOnly = fileInfo.IsReadOnly,
                LastWriteTime = fileInfo.LastWriteTime,
                LastWriteTimeUtc = fileInfo.LastWriteTimeUtc,
                Length = fileInfo.Length
            };
        }
    }
}
