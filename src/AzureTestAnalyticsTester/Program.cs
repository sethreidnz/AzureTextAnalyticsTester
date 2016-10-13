using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace AzureTestAnalyticsTester
{
    public class Program
    {
        private static TextAnalyticsClient _textAnalyticsClient;

        public static async void Main(string[] args)
        {
           var config = BuildConfiguration();

            var textAnalyticsKey = config["TextAnalytics:AzureSubscriptionKey"];
            _textAnalyticsClient = new TextAnalyticsClient(textAnalyticsKey);

            Console.WriteLine($"How are you feeling today?");
            var response = Console.ReadLine();

            Console.WriteLine($"You Responed with:");
            Console.WriteLine(response);
            var centimentScore = GetCentimentScore(response);

            WaitKey();
        }

        private static async Task<decimal?> GetCentimentScore(string text)
        {
            decimal? centimentScore = null;
            try
            {
                centimentScore = await _textAnalyticsClient.GetSentiment(text);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"There was an error calling the Azure Text Analytics api:");
                Console.WriteLine(JsonConvert.SerializeObject(exception));
            }
            return centimentScore;
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder();
            builder.AddUserSecrets("text-analytics-tester");

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddUserSecrets("text-analytics-tester")
                .Build();

            return config;
        }

        private static void WaitKey()
        {
            Console.WriteLine("Press ESC to stop");
            do
            {
                while (!Console.KeyAvailable)
                {
                    // Do something
                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }

        private static string GetConfigValue(string key)
        {
            return "";
        }
    }
}
