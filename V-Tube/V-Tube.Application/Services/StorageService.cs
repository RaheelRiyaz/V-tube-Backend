using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V_Tube.Application.Abstractions.IServices;

namespace V_Tube.Application.Services
{
    public class StorageService(string webrootPath) : IStorageService
    {
        public int DeleteLocalFile(string filePath)
        {
            File.Delete(filePath); 

            return 0;
        }

        public async Task<string> SaveLocalFile(IFormFile file)
        {
            var localFile = Path.Combine(GetPhysicalPath(), file.FileName);

            using FileStream fs = new FileStream(localFile,FileMode.Create);
            await file.CopyToAsync(fs);

            return localFile;
        }


        private string GetPhysicalPath()
        {
            string path = webrootPath + "/Files";

            if (!Path.Exists(path))
                Directory.CreateDirectory(path);

            return path;
        }
    }
}
