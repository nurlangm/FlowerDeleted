﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Fiorello.Helpers
{
    public class Helper
    {
        public static void DeleteImg(string root,string folder,string ImgName)
        {
            string fullPath = Path.Combine(root, folder, ImgName);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }
}
