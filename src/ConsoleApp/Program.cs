using AzureCloudSync.SyncEngine;
using System;
using System.Configuration;

namespace AzureCloudSync.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var accountName = ConfigurationManager.AppSettings["AccountName"];
            var accountKey = ConfigurationManager.AppSettings["AccountKey"];
            var containerName = ConfigurationManager.AppSettings["ContainerName"];
            var subscriberId = ConfigurationManager.AppSettings["SubscriberId"];
            var localStoragePath = ConfigurationManager.AppSettings["LocalStoragePath"];

            if (string.IsNullOrEmpty(accountName))
            {
                Console.WriteLine("Please enter your account name:");
                accountName = Console.ReadLine();
            }

            if (string.IsNullOrEmpty(accountKey))
            {
                Console.WriteLine("Please enter your account key:");
                accountKey = Console.ReadLine();
            }

            if (string.IsNullOrEmpty(containerName))
            {
                Console.WriteLine("Please enter your container name:");
                containerName = Console.ReadLine();
            }

            if (string.IsNullOrEmpty(subscriberId))
            {
                Console.WriteLine("Please enter your id:");
                subscriberId = Console.ReadLine();
            }

            var engine = new AzureBlobStorageSyncEngine(accountName, accountKey, containerName, subscriberId, localStoragePath);
            engine.RunAsync().Wait();

            Console.WriteLine("Sync completed...");
            Console.ReadKey(true);
        }
    }
}
