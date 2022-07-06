using Dapr.Client;
using System;
using System.Net.Http;
using System.Threading;

namespace StateManagementDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            SaveAndGetState();
            while (true)
            {
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// sdk 获取 设置 state状态信息不支持远程访问， 只能通过服务访问自己的sidecar来完成 并且通过grpc 的方式
        /// </summary>
        static async void SaveAndGetState()
        {

            string DAPR_STORE_NAME = "statestore";
            while (true)
            {

                var client = new DaprClientBuilder().Build();
                Random random = new Random();
                int orderId = random.Next(1, 1000);
                //Using Dapr SDK to save and get state
                
                await client.SaveStateAsync(DAPR_STORE_NAME, "order_1", orderId.ToString());
                await client.SaveStateAsync(DAPR_STORE_NAME, "order_2", orderId.ToString());
                var result = await client.GetStateAsync<string>(DAPR_STORE_NAME, "order_1");
                Console.WriteLine("Result after get: " + result);
                System.Threading.Thread.Sleep(5000);
            }
        }
    }
}
