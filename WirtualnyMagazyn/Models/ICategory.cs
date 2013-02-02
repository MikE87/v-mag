using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WirtualnyMagazyn.Models
{
    interface ICategory
    {
        IEnumerable<CategoryModel> GetAllCategories();
        IEnumerable<ProductModel> GetProductsFromCategory(int category);
        CategoryModel GetCategoryByID(int id);
        void AddCategory(CategoryModel category);
        void EditCategory(int id, CategoryModel category);
        void DeleteCategory(int id);
        int CountProductsInCategory(int category);
    }
}
