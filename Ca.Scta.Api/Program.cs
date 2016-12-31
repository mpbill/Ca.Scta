using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using Ca.Scta.Api.Controllers;
using Microsoft.Owin.Hosting;

namespace Ca.Scta.Api
{
    class Program
    {
        private static string baseAddress;
        static void Main(string[] args)
        {
            baseAddress = "http://localhost:9000/";
            using (WebApp.Start<AppStart.Startup>(baseAddress))
            {
                var client = new HttpClient();
                var loginModel = new LoginViewModel {UserName = "Admin", Password = "password"};
                Post("Account/Login",loginModel);

            }
            Console.ReadLine();
        }

        static CreateAccountViewModel GetCreateAccountViewModel()
        {
            return new CreateAccountViewModel
            {
                Email = "mpbill@gmail.com",
                Password = "password",
                ConfirmPassword = "password",
                UserName = "Admin"
            };
        }
        static void Post<T>(string route, T body)
        {
            var fullAddress = baseAddress + route;
            var client = new HttpClient();
            var response = client.PostAsync(fullAddress, new ObjectContent<T>(body, new JsonMediaTypeFormatter()));
            Console.WriteLine(response.Result.StatusCode);
            Console.WriteLine(response.Result.Content.ReadAsStringAsync().Result);
        }

        static void Get(string route)
        {

            var fullAddress = baseAddress + route;
            var client = new HttpClient();
            var response = client.GetAsync(fullAddress);
            Console.WriteLine(response.Result.StatusCode);
            Console.WriteLine(response.Result.Content.ReadAsStringAsync().Result);

        }
    }
}
