using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AzureTestAnalyticsTester
{
    public class TextAnalyticsClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _autherisationCredentials;
        public TextAnalyticsClient(string apiKey)
        {
            _autherisationCredentials = Base64Encode($"AccountKey:{apiKey}");
            _httpClient = new HttpClient();
        }

        public async Task<decimal> GetSentiment(string text)
        {
            return await Task.FromResult(0);
        }

        private static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}
