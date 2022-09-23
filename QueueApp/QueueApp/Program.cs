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

            // Retrieve the connection string for use with the application. The storage
            // connection string is stored in an environment variable called
            // AZURE_STORAGE_CONNECTION_STRING on the machine running the application.
            // If the environment variable is created after the application is launched
            // in a console or with Visual Studio, the shell or application needs to be
            // closed and reloaded to take the environment variable into account.
            string connectionString = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING");

            // Create a unique name for the queue
            string queueName = "quickstartqueues-" + Guid.NewGuid().ToString();

            Console.WriteLine($"Creating queue: {queueName}");

            // Instantiate a QueueClient which will be
            // used to create and manipulate the queue
            QueueClient queueClient = new QueueClient("DefaultEndpointsProtocol=https;AccountName=sa22sep;AccountKey=JpOi7lplh0rBPKegBf4FoZnvzioGrojstsONqjJvF51tgIoPzJuEXe1Cb/el0bD7NJaj3a7nCq3v+AStG/E0/g==;EndpointSuffix=core.windows.net", queueName);

            // Create the queue
            await queueClient.CreateAsync();


            Console.WriteLine("\nAdding messages to the queue...");

            // Send several messages to the queue
            await queueClient.SendMessageAsync("Heloo Guys");
            await queueClient.SendMessageAsync("Good Evening");

            // Save the receipt so we can update this message later
            SendReceipt receipt = await queueClient.SendMessageAsync("Third message");


            Console.WriteLine("\nPeek at the messages in the queue...");

            // Peek at messages in the queue
            PeekedMessage[] peekedMessages = await queueClient.PeekMessagesAsync(maxMessages: 10);

            foreach (PeekedMessage peekedMessage in peekedMessages)
            {
                // Display the message
                Console.WriteLine($"Message: {peekedMessage.MessageText}");
            }


            Console.WriteLine("\nUpdating the third message in the queue...");

            // Update a message using the saved receipt from sending the message
            await queueClient.UpdateMessageAsync(receipt.MessageId, receipt.PopReceipt, "How Are You!!");

        }
    }
}