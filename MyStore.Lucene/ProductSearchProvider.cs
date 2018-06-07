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
using MyStore.Business;
using Lucene.Net.Search;

namespace MyStore.Lucene
{
    public class ProductSearchProvider : SearchProvider<Product>
    {
        public ProductSearchProvider(DirectoryInfo directory) : base(directory)
        {
        }

        protected override string[] DefaultSearchFields => new string[]
        {
            ProductFields.Title,
            ProductFields.Id,
            ProductFields.Description
        };

        protected override string[] StoredFields => ProductFields.AllExcept(ProductFields.Description);
        
        protected override Document MapInstanceToDocument(Product instance)
        {
            var doc = new Document();

            doc.Add(new Field(ProductFields.Id, instance.Id, Field.Store.YES, Field.Index.NOT_ANALYZED));

            doc.Add(new Field(ProductFields.Description, instance.Description, Field.Store.NO, Field.Index.ANALYZED));

            doc.Add(new Field(ProductFields.Title, instance.Title, Field.Store.YES, Field.Index.ANALYZED));
            
            doc.Add(new Field(ProductFields.Added, instance.Added.ToString("o"), Field.Store.YES, Field.Index.NOT_ANALYZED));
            doc.Add(new Field(ProductFields.Banned, instance.Banned.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            doc.Add(new Field(ProductFields.Image, instance.Image, Field.Store.YES, Field.Index.NOT_ANALYZED));
            doc.Add(new Field(ProductFields.Price, instance.Price.ToString("000000000000.00"), Field.Store.YES, Field.Index.NOT_ANALYZED));
            doc.Add(new Field(ProductFields.SellsCount, instance.SellsCount.ToString("000000000000"), Field.Store.YES, Field.Index.NOT_ANALYZED));
            doc.Add(new Field(ProductFields.Stock, instance.Stock.ToString("000000000000"), Field.Store.YES, Field.Index.NOT_ANALYZED));

            return doc;
        }
    }
}
