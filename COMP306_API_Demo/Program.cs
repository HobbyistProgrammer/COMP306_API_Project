using COMP306_API_Demo.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace COMP306_API_Demo
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Trying to run console");
            await RunAsync();
            Console.WriteLine("Finsihed to run console");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public static async Task RunAsync()
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("http://3.144.176.28.nip.io");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("apikey", "wbTKfXmHWf84CCVAgHMOnmb3ZiICMAAd1zAOVDQOuG6eAK8u");

            try
            {
                Console.WriteLine("trying to get url contents");
                string json;
                HttpResponseMessage response = await client.GetAsync("/api/Products");
                if(response.IsSuccessStatusCode)
                {
                    Console.WriteLine("response to get url contents");
                    json = await response.Content.ReadAsStringAsync();
                    IEnumerable<ProductSummaryDto> items = JsonConvert.DeserializeObject<IEnumerable<ProductSummaryDto>>(json);
                    foreach(ProductSummaryDto item in items)
                    {
                        Console.WriteLine(item.ToString());
                    }
                }

                Console.WriteLine("Trying to get employees");

                json = "";
                HttpResponseMessage employeeResponse = await client.GetAsync("/api/Employees");
                if (employeeResponse.IsSuccessStatusCode)
                {
                    Console.WriteLine("response to get url contents");
                    json = await employeeResponse.Content.ReadAsStringAsync();
                    IEnumerable<EmployeeSummaryDto> items = JsonConvert.DeserializeObject<IEnumerable<EmployeeSummaryDto>>(json);
                    foreach (EmployeeSummaryDto item in items)
                    {
                        Console.WriteLine(item.ToString());
                    }
                }

            } catch (Exception e)
            {
                Console.WriteLine("Error has occured: ", e.Message);
            }
        }
    }
}