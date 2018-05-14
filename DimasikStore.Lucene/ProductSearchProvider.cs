using DimasikStore.Business.Entities;
using DimasikStore.SearchProvider;
using Lucene.Net.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimasikStore.Lucene
{
    public class ProductSearchProvider : SearchProvider<Product>
    {
        protected override string[] SearchFields => new string[]
        {
            "Title",
            "Description",
        };

        protected override Document MapInstanceToDocument(Product instance)
        {
            var doc = new Document();

            doc.Add(new Field("Id", instance.Id, Field.Store.YES, Field.Index.NOT_ANALYZED));

            doc.Add(new Field("Title", instance.Title, Field.Store.NO, Field.Index.ANALYZED));
            doc.Add(new Field("Description", instance.Description, Field.Store.NO, Field.Index.ANALYZED));

            return doc;
        }
    }
}
