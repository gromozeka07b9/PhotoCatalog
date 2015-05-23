using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoCatalogRenamer
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestZip();
            //return;
            int errorCount = 0;
            int maxErrorCount = 100;//если случилось что то серьезное, нет смысла обрабатывать все файлы, к примеру, сеть отвалилась
            CatalogList fi = new CatalogList();
            var list = fi.GetCurrentCatalogList();
            foreach (var curCatalog in list)
            {
                if (errorCount < maxErrorCount)
                {
                    if (fi.CatalogMustBeRenamed(new char[]{'_', '-'}, curCatalog.Name))
                    {
                        string oldCatalogName = curCatalog.Name;
                        string newCatalogName = string.Empty;
                        string error = string.Empty;
                        try
                        {
                            newCatalogName = fi.RenameCatalog(new char[]{'_', '-'}, curCatalog);
                        }
                        catch (Exception E)
                        {
                            error = E.Message;
                        }
                        if (string.IsNullOrEmpty(error))
                        {
                            Console.WriteLine("Каталог '" + oldCatalogName + "' переименован в '" + newCatalogName + "'");
                        }
                        else
                        {
                            Console.WriteLine("Ошибка переименования каталога '" + oldCatalogName + "', ошибка:'" + error + "'");
                            errorCount++;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Произошло слишком много ошибок");
                }
            }
            Console.ReadLine();
        }

        private static void TestZip()
        {
            using (ZipFile file = new ZipFile())
            {
                file.Password = "test";
                file.AlternateEncoding = Encoding.UTF8;
                file.AlternateEncodingUsage = ZipOption.Always;
                file.AddDirectory(@"Z:\2003");
                file.Save("test2003.zip");
            }
        }
    }
}
