using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProjectThird.Utilities.FIle
{
    public static class Extension
    {
        public static bool CheckFileType(this IFormFile file, string fileType)
        {
            return file.ContentType.Contains(fileType);
        }
        public  static bool CheckFileSize(this IFormFile file,int kbSize)
        {
            return file.Length / 1024 <= kbSize;
        }
        public async static Task<string> SaveFileAysnc(this IFormFile file, string root, string folder)
        {
            string fileName = Guid.NewGuid().ToString() + file.FileName;
            string resultPath = Path.Combine(root, folder, fileName);
            using (FileStream stream = new FileStream(resultPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return fileName;

        }

        
    }
}
