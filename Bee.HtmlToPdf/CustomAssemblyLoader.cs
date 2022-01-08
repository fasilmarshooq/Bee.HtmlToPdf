using System;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;

namespace Bee.HtmlToPdf
{
    public sealed class CustomAssemblyLoader : AssemblyLoadContext
    {
        private static bool IsLoaded { get; set; }
        private CustomAssemblyLoader()
        {
            LoadUnmanagedLibraryFromResources();
        }

        public static void Load()
        {
            if (!IsLoaded)
            {
                _ = new CustomAssemblyLoader();
                IsLoaded = true;
            }
        }

        public IntPtr LoadUnmanagedLibraryFromResources()
        {
            try
            {
                string tempDllLoaderPath = ExtractDll();
                return LoadUnmanagedDll(tempDllLoaderPath);
            }
            catch (Exception ex)
            {
                //Log.Fatal(ex.Message, ex);
            }
            return default;
        }

        private static string ExtractDll()
        {
            Assembly thisAssembly = Assembly.GetExecutingAssembly();
            var dll = Array.Find(thisAssembly.GetManifestResourceNames(), x => x.Contains("libwkhtmltox") && x.EndsWith(".dll"));

            var tempDllLoaderPath = Path.Combine(Directory.GetCurrentDirectory(), "libwkhtmltox.dll");

            if (File.Exists(tempDllLoaderPath)) return tempDllLoaderPath;

            using (Stream stm = Assembly.GetExecutingAssembly().GetManifestResourceStream(dll))
            {
                    using Stream outFile = File.Create(tempDllLoaderPath);
                    const int sz = 4096;
                    byte[] buf = new byte[sz];
                    while (true)
                    {
                        int nRead = stm.Read(buf, 0, sz);
                        if (nRead < 1)
                            break;
                        outFile.Write(buf, 0, nRead);
                    }
            }
            return tempDllLoaderPath;
        }

        protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
        {
            return LoadUnmanagedDllFromPath(unmanagedDllName);
        }
    }
}
