﻿using Dapr.Client;
using System;
using System.Threading;

namespace Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            string PUBSUB_NAME = "order-pub-sub";
            string TOPIC_NAME = "orders";
            while (true)
            {
                System.Threading.Thread.Sleep(5000);
                Random random = new Random();
                int orderId = random.Next(1, 1000);
                CancellationTokenSource source = new CancellationTokenSource();
                CancellationToken cancellationToken = source.Token;
                using var client = new DaprClientBuilder().Build();
                //Using Dapr SDK to publish a topic
                client.PublishEventAsync(PUBSUB_NAME, TOPIC_NAME, orderId, cancellationToken).Wait();
                Console.WriteLine("Published data: " + orderId);
            }
        }
    }
}
