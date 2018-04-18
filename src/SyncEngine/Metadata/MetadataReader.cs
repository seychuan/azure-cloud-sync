using AzureCloudSync.SyncEngine.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AzureCloudSync.SyncEngine.Metadata
{
    public static class MetadataReader
    {
        public static (DateTime, T) LoadFileInfoList<T>(string path, params string[] elements)
        {
            var dictionary = LoadDictionary(path, elements);

            if (dictionary == null) return (DateTime.MinValue, default(T));





            using (var reader = new StreamReader(path))
            {
                var dateStr = reader.ReadLine();
                var fileInfoStr = reader.ReadLine();

                if (string.IsNullOrEmpty(dateStr)) throw new Exception($"Missing date string in {path}!");
                if (string.IsNullOrEmpty(fileInfoStr)) throw new Exception($"Missing file info string in {path}!");

                DateTime dateTime = DateTime.FromBinary(long.Parse(dateStr));
                Serializer.DeserializeObjectFromBase64String<T>()


                var data = string.Empty;
                do
                {
                    data = reader.ReadLine();

                    var segments = data.Split('=');




                } (string.IsNullOrEmpty(data) == false);



            }


        }

        private static Dictionary<string, string> LoadDictionary(string path, params string[] elements)
        {
            if (File.Exists(path) == false) return null;

            var dictionary = new Dictionary<string, string>();

            using (var reader = new StreamReader(path))
            {
                do
                {
                    var data = reader.ReadLine();

                    if (string.IsNullOrEmpty(data)) continue;

                    if (data.IndexOf('=') < 0) throw new Exception($"Invalid data format in {path}!");

                    var segments = data.Split('=');

                    if (segments.Contains(segments[0]))
                    {
                        dictionary.Add(segments[0], segments[1]);
                    }

                } while (reader.EndOfStream == false);

                reader.Close();
            }

            return dictionary;
        }
    }
}
