using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureTestAnalyticsTester.Models
{
    public class TextDocumentModel
    {
        public string Language { get; set; }
        public string Id { get; set; }
        public string Text { get; set; }
        public decimal Score { get; set; }
    }
}
