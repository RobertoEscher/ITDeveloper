using System;
using System.IO;
using System.Reflection;

namespace Coopership.ITDeveloper.Application.Extensions
{
    public static class ImportUtils
    {
        public static string GetFilePath(string raiz, string filename, string extension)
        {
#pragma warning disable SYSLIB0012 // Type or member is obsolete
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
#pragma warning restore SYSLIB0012 // Type or member is obsolete
            var csvPath = Path.Combine(outPutDirectory,$"{raiz}\\{filename}{extension}");
            return new Uri(csvPath).LocalPath;
        }
    }
}
