using Azure;

using Azure.Storage.Queues;

using Azure.Storage.Queues.Models;

using System;

using System.Threading.Tasks;



namespace QueuesQuickstartV12

{

    class Program

    {

        static async Task Main(string[] args)

        {

            Console.WriteLine("Azure Queue Storage client library v12 - .NET quickstart sample\n");

            Console.WriteLine("Enter Queue Name");

            string queueName = Console.ReadLine();

            string connectionString = "DefaultEndpointsProtocol=https;AccountName=sa22sep;AccountKey=JpOi7lplh0rBPKegBf4FoZnvzioGrojstsONqjJvF51tgIoPzJuEXe1Cb/el0bD7NJaj3a7nCq3v+AStG/E0/g==;EndpointSuffix=core.windows.net";

            QueueClient queueClient = new QueueClient(connectionString, queueName);

            PeekedMessage[] peekedMessages = await queueClient.PeekMessagesAsync(maxMessages: 10);

            foreach (PeekedMessage peekedMessage in peekedMessages)

            {

                // Display the message

                Console.WriteLine($"Message: {peekedMessage.MessageText}");

            }

        }

    }
}