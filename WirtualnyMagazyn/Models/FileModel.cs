using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace WirtualnyMagazyn.Models
{
    public class DataSizeAttribute : ValidationAttribute
    {
        private int MaxDataSize;
        
        public DataSizeAttribute(int MaximumDataSize): base()
        {
            MaxDataSize = MaximumDataSize;
            this.ErrorMessage = "Maksymalny rozmiar pliku to " + MaximumDataSize + " Bajtów.";
        }

        public override bool IsValid(object value)
        {
            var data = value as HttpPostedFileBase;
            if (data == null)
                return true;
            if (data.ContentLength > this.MaxDataSize)
                return false;
            return true;
        }
    }

    public class AllowedContentTypeAttribute : ValidationAttribute
    {
        private string[] contentTypes;

        public AllowedContentTypeAttribute(string[] allowedContentTypes) : base()
        {
            contentTypes = allowedContentTypes;
            this.ErrorMessage = "Ten typ pliku jest niedozwolony.";
        }

        public override bool IsValid(object value)
        {
            var type = value as HttpPostedFileBase;
            if (type == null)
                return true;
            if (contentTypes.Contains(type.ContentType))
                return true;
            return false;
        }
    }

    [MetadataType(typeof(FileValidation))]
    public partial class FileModel
    {
        private string[] allowedDataTypes = { "image/jpeg", "image/png" };
        private string allowedDataTypesErrorMessage = "Tylko pliki JPEG i PNG";
        private int maxDataSize = 100 * 1024;
        private string maxDataSizeErrorMessage = "Maksymalny rozmiar pliku to 100 kB";

        public HttpPostedFileBase file { get; set; }
        
        // Walidacja dla Ajaxa
        public string Validate {
            get {
                if(this.Data == null || this.ContentType == null || this.Name == null)
                    return "Wybierz jakiś plik";
                if (this.Data.Length > this.maxDataSize)
                    return this.maxDataSizeErrorMessage;
                if(!this.allowedDataTypes.Contains(this.ContentType))
                    return this.allowedDataTypesErrorMessage;
                return null;
            }
        }
    }

    [Bind(Exclude = "ID, Name, ContentType, Data, ProductID")]
    public class FileValidation
    {
        [Required(ErrorMessage="Wybierz jakiś plik")]
        [DataSize(100 * 1024, ErrorMessage = "Maksymalny rozmiar pliku to 100 kB")]
        [AllowedContentType(new string[] { "image/jpeg", "image/png" }, ErrorMessage = "Tylko pliki JPEG i PNG")]
        public HttpPostedFileBase file { get; set; }
    }
}