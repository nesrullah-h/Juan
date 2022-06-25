﻿using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace EduHomeBackendim.Helpers
{
    public class Helper
    {
        
        public static void DeleteImage(IWebHostEnvironment webhost, string folder, string filename)
        {
            string path = webhost.WebRootPath;
            string resultPath = Path.Combine(path, folder, filename);
            if (System.IO.File.Exists(resultPath))
            {
                System.IO.File.Delete(resultPath);
            }
        }

    }
}
