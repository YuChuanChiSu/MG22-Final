using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericToolKit.Mvvm
{
    public static class GlobalConfiguration
    {
        private static readonly string _jsonPath = Path.Combine(UnityEngine.Application.persistentDataPath, "Json");
        
        public static string JsonPath
        {
            get
            {
                if (!Directory.Exists(_jsonPath))
                    Directory.CreateDirectory(_jsonPath);

                return _jsonPath;
            }
        }
    }
}
