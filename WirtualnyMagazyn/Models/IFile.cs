using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WirtualnyMagazyn.Models
{
    interface IFile
    {
        IEnumerable<FileModel> GetFiles(int id);
        void AddFile(FileModel file);
        void DeleteFile(int id);
    }
}
