using Dapr.Client;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Caller
{
    class TestType : IMessage<TestType>
    {
        public int A { get; set; }
        public int B { get; set; }

        public MessageDescriptor Descriptor => throw new NotImplementedException();

        public int CalculateSize()
        {
            throw new NotImplementedException();
        }

        public TestType Clone()
        {
            throw new NotImplementedException();
        }

        public bool Equals(TestType other)
        {
            throw new NotImplementedException();
        }

        public void MergeFrom(TestType message)
        {
            throw new NotImplementedException();
        }

        public void MergeFrom(CodedInputStream input)
        {
            throw new NotImplementedException();
        }

        public void WriteTo(CodedOutputStream output)
        {
            throw new NotImplementedException();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var client = new DaprClientBuilder().UseGrpcEndpoint("http://121.5.35.98:35995").Build();
            string appId = "webapp";

            //var sta = client.CheckHealthAsync().Result;
            var re = client.InvokeMethodGrpcAsync<TestType>(appId, "Service").Result;//{\"A\":1,\"B\":2}

        }
        static void CallServiceBySDKHttp()
        {
            var client = new DaprClientBuilder().UseHttpEndpoint("http://121.5.35.98:3500").Build();
            string appId = "webapp";

            //var sta = client.CheckHealthAsync().Result;
            var re = client.InvokeMethodAsync<TestType>(HttpMethod.Get, appId, "Service").Result;//{\"A\":1,\"B\":2}
        }
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
