using System;
using System.IO;
using System.Reflection;

namespace Coopership.ITDeveloper.Application.Extensions
{
    public static class ImportUtils
    {
        public static string GetFilePath(string raiz, string filename, string extension)
        {
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            var csvPath = Path.Combine(outPutDirectory,$"{raiz}\\{filename}{extension}");
            return new Uri(csvPath).LocalPath;
        }
    }
}
