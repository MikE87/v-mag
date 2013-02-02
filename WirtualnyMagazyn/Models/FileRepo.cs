using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WirtualnyMagazyn.Models
{
    public class FileRepo: IFile
    {
        DataClassesDataContext context = new DataClassesDataContext();

        public IEnumerable<FileModel> GetFiles(int id)
        {
            var files = from f in context.FileModels
                        where f.ProductID == id
                        select f;

            return files.ToList();
        }

        public void AddFile(FileModel file)
        {
            var f = from ff in context.FileModels
                    where ff.ProductID == file.ProductID
                    select ff;
            if (f.Count() > 0)
            {
                // ograniczenie do tylko jednego pliku
                var ph = context.FileModels.Single(x => x.ProductID == file.ProductID);
                ph.Name = file.Name;
                ph.ContentType = file.ContentType;
                ph.ProductID = file.ProductID;
                ph.Data = file.Data;
            }
            else
            {
                FileModel fm = new FileModel();
                fm.Name = file.Name;
                fm.ContentType = file.ContentType;
                fm.ProductID = file.ProductID;
                fm.Data = file.Data;
                context.FileModels.InsertOnSubmit(fm);
            }

            context.SubmitChanges();
        }

        public void DeleteFile(int id)
        {
            var files = from f in context.FileModels
                        where f.ProductID == id
                        select f;

            context.FileModels.DeleteAllOnSubmit(files);
            context.SubmitChanges();
        }

        public bool ValidateImageID(int id)
        {
            if (id > 0 && (context.FileModels.Any(f => f.ProductID == id)))
                return true;
            return false;
        }

        public bool ValidateProductID(int id)
        {
            if (id > 0 && (context.ProductModels.Any(f => f.ID == id)))
                return true;
            return false;
        }
    }
}