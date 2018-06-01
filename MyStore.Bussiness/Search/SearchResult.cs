using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Business.Search
{
    public class SearchResult
    {
        public SearchResult(Dictionary<string, string> dictionary, string[] resultFields)
        {
            this.dictionary = dictionary;
            ResultFields = resultFields;
        }

        private Dictionary<string, string> dictionary;

        public string[] ResultFields { get; private set; }

        public string GetValue(string fieldName)
        {
            if (!ResultFields.Contains(fieldName))
                throw new InvalidOperationException("Invalid fieldName");

            return dictionary[fieldName];
        }
    }
}
