using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Lucene
{
    public partial class Configuration
    {
        public static int SearchResultsDefaultLimit => int.Parse(ConfigurationManager.AppSettings["SearchResultsDefaultLimit"]);
    }
}
