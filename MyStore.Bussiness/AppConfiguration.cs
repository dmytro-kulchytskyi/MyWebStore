using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Business
{
    public static class AppConfiguration
    {
        public static string SearchManagerFolderName => ConfigurationManager.AppSettings["Business.SearchManager.FolderName"];
    }
}
