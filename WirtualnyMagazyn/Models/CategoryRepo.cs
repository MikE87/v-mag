using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WirtualnyMagazyn.Models
{
    public class CategoryRepo : ICategory
    {
        DataClassesDataContext context = new DataClassesDataContext();

        public IEnumerable<ProductModel> GetProductsFromCategory(int category)
        {
            var products = from p in context.ProductModels
                           where p.Category == category
                           select p;

            return products.ToList();
        }

        public IEnumerable<CategoryModel> GetAllCategories()
        {
            return context.CategoryModels;
        }

        public CategoryModel GetCategoryByID(int id)
        {
            return context.CategoryModels.Single(c => c.ID == id);
        }

        public void AddCategory(CategoryModel category)
        {
            if (category.Description == null)
                category.Description = "";
            context.CategoryModels.InsertOnSubmit(category);
            context.SubmitChanges();
        }

        public void EditCategory(int id, CategoryModel category)
        {
            var cat = context.CategoryModels.Single(c => c.ID == id);
            cat.Name = category.Name;
            if (category.Description != null)
                cat.Description = category.Description;
            else
                cat.Description = "";

            context.SubmitChanges();
        }

        public void DeleteCategory(int id)
        {
            var cat = context.CategoryModels.Single(c => c.ID == id);
            var prods = this.GetProductsFromCategory(id);
            var prodIDs = from p in context.ProductModels
                          where p.Category == id
                          select p.ID;
            var files = from f in context.FileModels
                        where prodIDs.Contains(f.ProductID)
                        select f;

            context.FileModels.DeleteAllOnSubmit(files);
            context.ProductModels.DeleteAllOnSubmit(prods);
            context.CategoryModels.DeleteOnSubmit(cat);
            context.SubmitChanges();
        }

        public int CountProductsInCategory(int category)
        {
            return context.ProductModels.Count(p => p.Category == category);
        }

        public bool ValidateID(int id)
        {
            if (id > 0 && (context.CategoryModels.Any(p => p.ID == id)))
                return true;
            return false;
        }
    }
}