using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel;

namespace WirtualnyMagazyn.Models
{
    [MetadataType(typeof(ProductValidation))]
    public partial class ProductModel
    {
        private FileRepo fr = new FileRepo();

        public bool hasImage {
            get {
                if (fr.GetFiles(this.ID).Count() > 0)
                    return true;
                return false;
            }
        }

        public string PhotoName { 
            get {
                if (fr.GetFiles(this.ID).Count() > 0)
                    return fr.GetFiles(this.ID).First().Name;
                return "Brak zdjęcia";
            } 
        }
    }

    [Bind(Exclude = "ID,Category")]
    public class ProductValidation
    {
        [Required(ErrorMessage = "To pole jest wymagane")]
        [StringLength(25, MinimumLength=3, ErrorMessage = "Od 3 do 25 znaków")]
        [RegularExpression("[0-9a-zA-ZąęółśćńźżĄĘÓŁŚĆŃŹŻ_+,.$€@*-]+", ErrorMessage = "Tylko cyfry, litery i znaki: ( _ + - . $ € @ * )")]
        [DisplayName("Nazwa")]
        public string Name { get; set; }

        [StringLength(100, ErrorMessage = "Maksymalnie 100 znaków")]
        [DisplayName("Opis")]
        public string Description { get; set; }

        [Range(0, 1000, ErrorMessage="Jedynie wartość z zakresu od 0 do 1000")]
        [DisplayName("Stan Magazynowy")]
        public int Count { get; set; }

        [Range(0, 1000000, ErrorMessage="Jedynie wartość z zakresu od 0 do 1000000")]
        [DataType(DataType.Currency)]
        [DisplayName("Cena")]
        public decimal Price { get; set; }
    }
}