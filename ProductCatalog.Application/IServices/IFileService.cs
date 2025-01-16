using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.Application.IServices
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(IFormFile file, string folderName);
        string GetUploadsFolderPath(string folderName);
    }

}
