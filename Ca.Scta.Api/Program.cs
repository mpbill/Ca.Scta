using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace Ca.Scta.Api
{
    class Program
    {
        static void Main(string[] args)
        {
            const string baseAddress = "http://localhost:9000/";
            using (WebApp.Start<AppStart.Startup>(baseAddress))
            {
                var client = new HttpClient();
                var response = client.GetAsync(baseAddress + "api/values/1");
                Console.Write(response.Result.Content.ReadAsStringAsync().Result);
            }
            Console.ReadLine();
        }
    }
}
