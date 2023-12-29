using System;
using System.Net;
using System.Net.Http;
namespace Assignment;
class Program{
    public static void Main(string[] args)
    {
        Operation operation = new Operation();
        operation.AllOperation(args[0]);
        string url = "https://www.your-actual-url.com";
         using (HttpClient client = new HttpClient())
        {
            // Update the SSL/TLS configuration to use TLS 1.2
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            try
            {
                // Make the GET request
                HttpResponseMessage response = client.GetAsync(url).Result;

                // Check the response
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Connection successful!");
                    // Process the response data as needed
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.Content.ReadAsStringAsync().Result}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
    }
}   