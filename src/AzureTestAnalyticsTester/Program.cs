using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AzureTestAnalyticsTester.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace AzureTestAnalyticsTester
{
    public class Program
    {
        private static TextAnalyticsClient _textAnalyticsClient;

        public static void Main(string[] args)
        {
            // call async version of main method
            Main().Wait();
        }

        public static async Task Main()
        {
            var config = BuildConfiguration();

            var textAnalyticsKey = config["TextAnalytics:AzureSubscriptionKey"];
            _textAnalyticsClient = new TextAnalyticsClient(textAnalyticsKey);

            Console.WriteLine($"How are you feeling today?");
            var textToAnalyse = Console.ReadLine();

            var sentimentScore = await GetSentimentScore(textToAnalyse);

            Console.WriteLine($"The sentiment score of the text entered: '{textToAnalyse}' is:");
            Console.WriteLine(sentimentScore);

            WaitKey();
        }

        private static async Task<decimal?> GetSentimentScore(string text)
        {
            TextDocumentModel textDocument = null;
            try
            {
                textDocument = await _textAnalyticsClient.GetSentimentScore(text);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"There was an error calling the Azure Text Analytics api:");
                Console.WriteLine(JsonConvert.SerializeObject(exception));
            }

            decimal? score = null;
            if (textDocument != null)
            {
                score = textDocument.Score;
            }

            return score;
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
            Console.WriteLine("Press ESC to quit");
            do
            {
                while (!Console.KeyAvailable)
                {
                    // Do something
                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }
}
