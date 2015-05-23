using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoCatalogRenamer
{
    public class CatalogList
    {
        string dateDelimiter = ".";
        public struct NewCatalog
        {
            public string catalogStringDate;
            public DateTime catalogDate;
            public string catalogName;
        }

        public CatalogList()
        {

        }

        public DirectoryInfo[] GetCurrentCatalogList()
        {
            DirectoryInfo di = new DirectoryInfo(Environment.CurrentDirectory);
            DirectoryInfo[] list = di.GetDirectories("*", SearchOption.AllDirectories);
            return list;
        }

        public bool CatalogMustBeRenamed(char[] cDelimiters, string curCatalogName)
        {
            //если наименование каталога с разделителем то считаем что это наш кандидат
            foreach (char item in cDelimiters)
            {
                if (curCatalogName.Contains(item)) return true;
            }
            return false;
        }

        public string RenameCatalog(char[] cDelimiters, DirectoryInfo curCatalog)
        {
            string oldName = curCatalog.Name;
            string newName = string.Empty;
            foreach (char delimiter in cDelimiters)
            {
                NewCatalog newCatalogData = GetNewCatalog(oldName, delimiter);
                if (!string.IsNullOrEmpty(newCatalogData.catalogName))
                {
                    /*curCatalog.MoveTo(newCatalogData.catalogName);
                    curCatalog.CreationTime = newCatalogData.catalogDate;
                    curCatalog.LastAccessTime = curCatalog.CreationTime;
                    curCatalog.LastWriteTime = curCatalog.CreationTime;*/
                }
            }
            /*NewCatalog delimiterResult = GetDateForDelimiter(oldName, '_');
            if (string.IsNullOrEmpty(delimiterResult.catalogStringDate))
            {
                delimiterResult = GetDateForDelimiter(oldName, '-');
            }
            if (!string.IsNullOrEmpty(delimiterResult.catalogStringDate))
            {
                newName = delimiterResult.catalogStringDate + "_" + delimiterResult.catalogNameWithoutDate;
            }

            if ((oldName != newName) && (!string.IsNullOrEmpty(newName)))
            {
                curCatalog.MoveTo(newName);
                curCatalog.CreationTime = delimiterResult.catalogDate;
                curCatalog.LastAccessTime = curCatalog.CreationTime;
                curCatalog.LastWriteTime = curCatalog.CreationTime;
            }
            else
            {
                throw new Exception("Определить дату не удалось");
            }*/

            return newName;
        }

        public NewCatalog GetNewCatalog(string oldName, char charDelimiter)
        {
            NewCatalog result = new NewCatalog() { catalogStringDate = string.Empty, catalogName = string.Empty};
            string[] splittedNameArr = oldName.Split(charDelimiter);
            if (splittedNameArr.Length > 0)
            {
                StringBuilder sbDatePartOfName = new StringBuilder();
                StringBuilder otherPartOfName = new StringBuilder();
                DateTime fileDate = new DateTime();
                foreach (string namePart in splittedNameArr)
                {
                    StringFormatter formatter = new StringFormatter();
                    DateTime foundedDate = formatter.tryConvertToDate(namePart);
                    if (foundedDate.Year > 1900)
                    {
                        if (sbDatePartOfName.Length > 0)
                        {
                            sbDatePartOfName.Append("_");
                        }
                        sbDatePartOfName.Append(foundedDate.ToString("yyyy" + dateDelimiter + "MM" + dateDelimiter + "dd"));
                        fileDate = foundedDate;
                    }
                    else
                    {
                        otherPartOfName.Append(charDelimiter);
                        otherPartOfName.Append(namePart);
                    }
                }
                if (sbDatePartOfName.Length > 0)
                {
                    result.catalogStringDate = sbDatePartOfName.ToString();
                    result.catalogDate = fileDate;
                    result.catalogName = result.catalogStringDate + otherPartOfName.ToString();

                }
            }
            return result;
        }
    }
}
