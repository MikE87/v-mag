using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WirtualnyMagazyn.Models
{
    interface IProduct
    {
        IEnumerable<ProductModel> GetProductsFromCategory(int category);
        ProductModel GetProductByID(int id);
        void AddProduct(ProductModel product);
        void EditProduct(int id, ProductModel product);
        void SetProductsInStock(int id, int count);
        void DeleteProduct(int id);
    }
}
