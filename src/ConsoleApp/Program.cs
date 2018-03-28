using AzureCloudSync.SyncEngine;
using System;

namespace AzureCloudSync.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var engine = new AzureBlobStorageSyncEngine("", "");
            engine.RunAsync().Wait();

            Console.WriteLine("Sync completed...");
            Console.ReadKey(true);
        }
    }
}
