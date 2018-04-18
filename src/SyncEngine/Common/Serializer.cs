using Newtonsoft.Json;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace AzureCloudSync.SyncEngine.Common
{
    public static class Serializer
    {
        public static string SerializeObjectToBase64String(object obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                new BinaryFormatter().Serialize(ms, obj);
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        public static object DeserializeBase64StringToObject(string base64String)
        {
            byte[] bytes = Convert.FromBase64String(base64String);
            using (MemoryStream ms = new MemoryStream(bytes, 0, bytes.Length))
            {
                ms.Write(bytes, 0, bytes.Length);
                ms.Position = 0;
                return new BinaryFormatter().Deserialize(ms);
            }
        }

        public static T DeserializeObjectFromPath<T>(string path)
        {
            if (File.Exists(path) == false) return default(T);

            using (var reader = new StreamReader(path))
            {
                var data = reader.ReadLine();

                return DeserializeObjectFromBase64String<T>(data);
            }
        }

        public static T DeserializeObjectFromBase64String<T>(string value)
        {
            // Decode based 64 string to serialized myfileinfo
            var jsonData = DeserializeBase64StringToObject(value).ToString();

            // Deserialize myfileinfo
            return JsonConvert.DeserializeObject<T>(jsonData);
        }
    }
}
