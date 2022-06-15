using Dapr.Client;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Caller
{
    class TestType
    {
        public int A { get; set; }
        public int B { get; set; }

    }
    class Program
    {
        static void Main(string[] args)
        {

            CallServiceBySDKHttp();
        }
        /// <summary>
        /// 使用dapr sdk 来完成服务调用
        /// </summary>
        static void CallServiceBySDKHttp()
        {
            //var client = new DaprClientBuilder().UseHttpEndpoint("http://121.5.35.98:3500").Build();
            var client = new DaprClientBuilder().Build();
            string appId = "webapp";

            //var sta = client.CheckHealthAsync().Result;
            
            var re = client.InvokeMethodAsync<TestType>(HttpMethod.Get, appId, "Service").Result;//{\"A\":1,\"B\":2}
            Console.WriteLine(re.A.ToString() + "  " + re.B.ToString());
        }
        /// <summary>
        /// 通过组装 http请求的方式调用服务
        /// </summary>
        static void CallServiceByHttp()
        {
            var baseURL = (Environment.GetEnvironmentVariable("BASE_URL") ?? "http://121.5.35.98") + ":" + (Environment.GetEnvironmentVariable("DAPR_HTTP_PORT") ?? "3500");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            // Adding app id as part of the header
            client.DefaultRequestHeaders.Add("dapr-app-id", "webapp");

            for (int i = 1; i <= 10; i++)
            {

                // Invoking a service
                HttpResponseMessage response = client.GetAsync($"{baseURL}/service").Result;


                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                Thread.Sleep(1000);

            }
        }

    }
}
