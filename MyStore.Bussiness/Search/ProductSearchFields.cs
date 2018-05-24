using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Business.Search
{
    public static class ProductSearchFields
    {
        public const string Id = "Id";
        public const string Title = "Title";
        public const string ExternalProductId = "ExternalProductId";
        public const string Image = "Image";
        public const string Description = "Description";
        public const string Price = "Prise";
        public const string SellsCount = "SellsCount";
        public const string Banned = "Banned";
        public const string Stock = "Stock";
        public const string Added = "Added";

        public static string[] All => typeof(ProductSearchFields).GetFields()
            .Where(f => f.IsLiteral && !f.IsInitOnly)
            .Select(p => (string)p.GetRawConstantValue())
            .ToArray();

        public static string[] AllExcept(params string[] fields) =>
            All.Where(f => !fields.Contains(f)).ToArray();
    }
}
