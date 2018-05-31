using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Business
{
    public static class ProductFields
    {
        public const string Id = "Id";
        public const string Title = "Title";
        public const string Image = "Image";
        public const string Description = "Description";
        public const string Price = "Price";
        public const string SellsCount = "SellsCount";
        public const string Banned = "Banned";
        public const string Stock = "Stock";
        public const string Added = "Added";

        public static string[] All => new string[]
        {
            Id,
            Title,
            Image,
            Description,
            Price,
            SellsCount,
            Banned,
            Stock,
            Added
        };

        public static string[] AllExcept(params string[] fields) =>
            All.Where(f => !fields.Contains(f)).ToArray();
    }
}
