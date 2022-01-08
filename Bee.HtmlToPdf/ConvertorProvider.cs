using DinkToPdf;
using DinkToPdf.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bee.HtmlToPdf
{
    public static class ConvertorProvider
    {
        private static IConverter Convertor = null;

        public static IConverter GetConvertor()
        {
            return Convertor ??= new SynchronizedConverter(new PdfTools());
        }
    }
}
