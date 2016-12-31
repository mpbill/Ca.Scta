using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Ca.Scta.Api.Controllers;
using Ca.Scta.Api.Controllers.Account;
using Ca.Scta.Api.Controllers.Account.Models;
using Microsoft.Owin.Hosting;
using Newtonsoft.Json;

namespace Ca.Scta.Api
{
    class Server
    {
        private static string baseAddress;
        static void Main(string[] args)
        {
            baseAddress = "http://localhost:9000/";
            using (WebApp.Start<AppStart.Startup>(baseAddress))
            {
                var client = new HttpClient();
                var loginModel = new LoginViewModel {UserName = "Admin", Password = "password"};
                var tokenResp = Post("Account/Login",loginModel);
                var tokenRespObj = DeSerialize<TokenResponse>(tokenResp);
                GetAuth("Account/UserInfo", tokenRespObj.Token);

            }
            Console.ReadLine();
        }

        static T DeSerialize<T>(string s)
        {
            StringReader sr = new StringReader(s);
            JsonSerializer ser = JsonSerializer.Create();
            T deserialized = ser.Deserialize<T>(new JsonTextReader(sr));
            return deserialized;
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
        static string Post<T>(string route, T body)
        {
            var fullAddress = baseAddress + route;
            var client = new HttpClient();
            
            var response = client.PostAsync(fullAddress, new ObjectContent<T>(body, new JsonMediaTypeFormatter()));
            Console.WriteLine(response.Result.StatusCode);
            var stringResp = response.Result.Content.ReadAsStringAsync().Result;
            Console.WriteLine(stringResp);
            return stringResp;

        }

        static void GetAuth(string route, string token)
        {
            var fullAddress = baseAddress + route;
            var client = new HttpClient();
            var requestMessage = new HttpRequestMessage(HttpMethod.Get,fullAddress);
            requestMessage.Headers.Add("Authorization", $"Bearer {token}");
            var response = client.SendAsync(requestMessage);
            Console.WriteLine(response.Result.StatusCode);
            var stringResp = response.Result.Content.ReadAsStringAsync().Result;
            Console.WriteLine(stringResp);
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
