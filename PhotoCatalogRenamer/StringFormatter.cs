using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoCatalogRenamer
{
    public class StringFormatter
    {
        public DateTime tryConvertToDate(string namePart)
        {
            DateTime resultDate = DateTime.FromBinary(0);
            string[] formatDateList = { "ddMMyyyy", "dd_MM_yyyy", "dd-MM-yyyy", "dd-MM_yyyy", "dd_MM-yyyy" };
            foreach (string formatDate in formatDateList)
            {
                try
                {
                    resultDate = DateTime.ParseExact(namePart, formatDate, CultureInfo.InvariantCulture);
                    break;
                }
                catch (Exception)
                {
                }
            }
            return resultDate;
        }
    }
}
