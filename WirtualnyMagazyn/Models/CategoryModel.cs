using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel;

namespace WirtualnyMagazyn.Models
{
    public class UniqueAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var val = value as string;
            if (val == null)
                return true;
            CategoryRepo cr = new CategoryRepo();
            if (cr.GetAllCategories().Any(c => c.Name.Equals(val)))
                return false;
            return true;
        }
    }

    [MetadataType(typeof(CategoryValidation))]
    public partial class CategoryModel
    {
        private CategoryRepo cr = new CategoryRepo();
        public int selectedCategory { get; set; }

        public int CountProducts
        {
            get { return cr.CountProductsInCategory(this.ID); }
        }

        public SelectList CategoryList
        {
            get { return new SelectList(cr.GetAllCategories(), "ID", "Name", this.selectedCategory); }
        }
    }

    [Bind(Exclude = "ID")]
    public class CategoryValidation
    {
        [Required(ErrorMessage = "To pole jest wymagane")]
        [StringLength(25, MinimumLength=3, ErrorMessage = "Od 3 do 25 znaków")]
        [RegularExpression("[0-9a-zA-ZąęółśćńźżĄĘÓŁŚĆŃŹŻ_+,.$€@*-]+", ErrorMessage="Tylko cyfry, litery i znaki: ( _ + - . $ € @ * )")]
        [DisplayName("Nazwa")]
        public string Name { get; set; }

        [StringLength(100, ErrorMessage = "Maksymalnie 100 znaków")]
        [DisplayName("Opis")]
        public string Description { get; set; }
    }
}