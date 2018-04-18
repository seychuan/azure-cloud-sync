using Newtonsoft.Json;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace AzureCloudSync.FileInfo
{
    public static class CustomFileInfoExtension
    {
        public static CustomFileInfo Clone(this CustomFileInfo fileInfo)
        {
            return new CustomFileInfo()
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

        public static string EncodeCustomFileInfo(this CustomFileInfo fileInfo, string localStoragePath)
        {
            var fileInfoClone = fileInfo.Clone();

            // Remove local storage path
            fileInfoClone.FullPath = fileInfoClone.FullPath.Replace(localStoragePath, string.Empty).TrimStart('\\');
            fileInfoClone.DirectoryFullPath = fileInfoClone.DirectoryFullPath.Replace(localStoragePath, string.Empty).TrimStart('\\');

            // Serialize myfileinfo into string
            var jsonData = JsonConvert.SerializeObject(fileInfoClone);

            // Encode serialized myfileinfo into based 64 string
            var encodedString = SerializeObjectToString(jsonData);

            // Remove encoding header
            return encodedString.Replace("AAEAAAD/////", "");
        }

        //public static CustomFileInfo DecodeBlobName(string blobName, string localStoragePath)
        //{
        //    // Append back encoding header
        //    var encodedString = "AAEAAAD/////" + blobName;

        //    // Decode based 64 string to serialized myfileinfo
        //    var jsonData = DeserializeStringToObject(encodedString).ToString();

        //    // Deserialize myfileinfo
        //    var fileInfo = JsonConvert.DeserializeObject<CustomFileInfo>(jsonData);

        //    // Append local storage path
        //    fileInfo.FullPath = Path.Combine(localStoragePath, fileInfo.FullPath.TrimStart('\\'));
        //    fileInfo.DirectoryFullPath = Path.Combine(localStoragePath, fileInfo.DirectoryFullPath.TrimStart('\\'));

        //    return fileInfo;
        //}

        public static CustomFileInfo DecodeFromBlobName(this CustomFileInfo fileInfo, string blobName, string localStoragePath)
        {
            // Append back encoding header
            var encodedString = "AAEAAAD/////" + blobName;

            // Decode based 64 string to serialized myfileinfo
            var jsonData = DeserializeStringToObject(encodedString).ToString();

            // Deserialize myfileinfo
            fileInfo = JsonConvert.DeserializeObject<CustomFileInfo>(jsonData);

            // Append local storage path
            fileInfo.FullPath = Path.Combine(localStoragePath, fileInfo.FullPath.TrimStart('\\'));
            fileInfo.DirectoryFullPath = Path.Combine(localStoragePath, fileInfo.DirectoryFullPath.TrimStart('\\'));

            return fileInfo;
        }
    }
}
