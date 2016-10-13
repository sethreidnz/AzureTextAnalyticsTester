using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AzureTestAnalyticsTester.Models;
using Newtonsoft.Json;

namespace AzureTestAnalyticsTester
{
    public class TextAnalyticsClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _sentimentAnalysisEndPoint = "https://westus.api.cognitive.microsoft.com/text/analytics/v2.0/sentiment";

        public TextAnalyticsClient(string apiKey)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", apiKey);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<TextDocumentModel> GetSentimentScore(string text)
        {
            var document = GenerateTextDocument(text);
            return await AnalyseTextDocumentSentiment(document);
        }

        public async Task<TextDocumentModel> AnalyseTextDocumentSentiment(TextDocumentModel document)
        {
            var requestObject = new TextDocumentRequestModel()
            {
                Documents = new List<TextDocumentModel>() {document}
            };
               
            var request = new HttpRequestMessage(HttpMethod.Post, _sentimentAnalysisEndPoint)
            {
                Content = new StringContent(JsonConvert.SerializeObject(requestObject), Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);

            var jsonString = await response.Content.ReadAsStringAsync();
            var deserializedResponse = JsonConvert.DeserializeObject<TextDocumentRequestModel>(jsonString);

            return deserializedResponse.Documents.Any() ? deserializedResponse.Documents[0] : null;
        }

        private TextDocumentModel GenerateTextDocument(string text, string language = "en")
        {
            return new TextDocumentModel()
            {
                Text = text,
                Language = language,
                Id = Guid.NewGuid().ToString()
            };
        }
    }
}
