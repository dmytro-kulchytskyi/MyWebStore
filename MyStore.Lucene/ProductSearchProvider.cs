using MyStore.Business.Entities;
using MyStore.SearchProvider;
using Lucene.Net.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MyStore.Business.Search;

namespace MyStore.Lucene
{
    public class ProductSearchProvider : SearchProvider<Product>
    {
        public ProductSearchProvider(DirectoryInfo directory) : base(directory)
        {
        }

        protected override string[] DefaultSearchFields => new string[]
        {
            ProductSearchFields.Title,
            ProductSearchFields.Description,
            ProductSearchFields.ExternalProductId
        };

        protected override string[] StoredFields => ProductSearchFields.AllExcept(ProductSearchFields.Description);

        protected override Document MapInstanceToDocument(Product instance)
        {
            var doc = new Document();

            doc.Add(new Field(ProductSearchFields.Id, instance.Id, Field.Store.YES, Field.Index.NOT_ANALYZED));

            doc.Add(new Field(ProductSearchFields.Description, instance.Description, Field.Store.NO, Field.Index.ANALYZED));

            doc.Add(new Field(ProductSearchFields.Title, instance.Title, Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field(ProductSearchFields.ExternalProductId, instance.ExternalProductId, Field.Store.YES, Field.Index.ANALYZED));

            doc.Add(new Field(ProductSearchFields.Added, instance.Added.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            doc.Add(new Field(ProductSearchFields.Banned, instance.Banned.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            doc.Add(new Field(ProductSearchFields.Image, instance.Image, Field.Store.YES, Field.Index.NOT_ANALYZED));
            doc.Add(new Field(ProductSearchFields.Price, instance.Price.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            doc.Add(new Field(ProductSearchFields.SellsCount, instance.SellsCount.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            doc.Add(new Field(ProductSearchFields.Stock, instance.Stock.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));

            return doc;
        }
    }
}
