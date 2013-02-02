using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WirtualnyMagazyn.Models
{
    public class ProductRepo : IProduct
    {
        DataClassesDataContext context = new DataClassesDataContext();


        public IEnumerable<ProductModel> GetProductsFromCategory(int category)
        {
            var products = from p in context.ProductModels
                           where p.Category == category
                           select p;

            return products.ToList();
        }

        public ProductModel GetProductByID(int id)
        {
            return context.ProductModels.Single(p => p.ID == id);
        }

        public void AddProduct(ProductModel product)
        {
            context.ProductModels.InsertOnSubmit(product);
            context.SubmitChanges();
        }

        public void EditProduct(int id, ProductModel product)
        {
            var pr = context.ProductModels.Single(p => p.ID == id);

            pr.Name = product.Name;
            pr.Description = product.Description;
            pr.Count = product.Count;
            pr.Price = product.Price;

            context.SubmitChanges();
        }

        public void SetProductsInStock(int id, int count)
        {
            var product = context.ProductModels.Single(p => p.ID == id);
            product.Count = count;

            context.SubmitChanges();
        }

        public void DeleteProduct(int id)
        {
            var product = context.ProductModels.Single(p => p.ID == id);
            var files = from f in context.FileModels
                        where f.ProductID == id
                        select f;

            context.FileModels.DeleteAllOnSubmit(files);
            context.ProductModels.DeleteOnSubmit(product);
            context.SubmitChanges();
        }

        public bool ValidateID(int id)
        {
            if (id > 0 && (context.ProductModels.Any(p => p.ID == id)))
                return true;
            return false;
        }
    }
}