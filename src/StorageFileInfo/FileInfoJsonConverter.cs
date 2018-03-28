using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace AzureCloudSync.StorageFileInfo
{
    public class FileInfoJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(FileInfo).IsAssignableFrom(objectType);
        }

        public override bool CanRead
        {
            get { return false; }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            JToken fileToken = JToken.FromObject(value);

            if (fileToken.Type != JTokenType.Object)
            {
                fileToken.WriteTo(writer);
            }
            else
            {
                // Below is the example from website
                // https://www.newtonsoft.com/json/help/html/CustomJsonConverter.htm
                //IList<string> propertyNames = o.Properties().Select(p => p.Name).ToList();
                //o.AddFirst(new JProperty("Keys", new JArray(propertyNames)));

                FileInfo fileInfo = (FileInfo)value;

                JObject fileObject = (JObject)fileToken;

                fileObject.Add("DirectoryFullPath", fileInfo.Directory.FullName);
                fileObject.Add("Extension", fileInfo.Extension);
                fileObject.Add("IsReadOnly", fileInfo.IsReadOnly);
                fileObject.Add("LastWriteTime", fileInfo.LastWriteTime);
                fileObject.Add("LastWriteTimeUtc", fileInfo.LastWriteTimeUtc);
                fileObject.Add("Length", fileInfo.Length);

                fileObject.WriteTo(writer);
            }
        }
    }
}
